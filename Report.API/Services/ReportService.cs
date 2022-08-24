using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Report.API.Constants;
using Report.API.Data;
using Report.API.Enums;
using Report.API.Models.Context;
using Report.API.Models.Entity;
using Report.API.Services.Interfaces;
using System.Numerics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using ReportRequestData = Report.API.Data.ReportRequestData;

namespace Report.API.Services
{
    public class ReportService:IReportService
    {
        ReportDbContext reportContext;
        private readonly ReportSettings reportSettings;
        private readonly IHttpClientFactory httpClientFactory;

        public ReportService(ReportDbContext context, IHttpClientFactory httpClientFactory, IOptions<ReportSettings> reportSettings)
        {
            reportContext = context;
            this.httpClientFactory = httpClientFactory;
            this.reportSettings = reportSettings?.Value; 
        }

        public async Task<ReportReturnData> CreateNewReport()
        {
            var report = new Report.API.Models.Entity.Report()
            {
                requestDate = DateTime.UtcNow,
                reportStatus = ReportStatusType.preparing,
                reportPath="",
            };

            await reportContext.reportTable.AddAsync(report);
            await reportContext.SaveChangesAsync();

            if (report.uuId != Guid.Empty)
            {
                return new ReportReturnData
                {
                    response = true,
                    message = "Rapor kaydı eklendi.",
                    data = report
                };
            }
            else
            {
                return new ReportReturnData
                {
                    response = false,
                    message = "Rapor kaydı eklenemedi.",
                    data = null
                };
            }
        }

        public async Task<ReportReturnData> GetAllReports()
        {
           
            var allReports = await reportContext.reportTable.Select(p => new ReportData
            {
                uuId = p.uuId,
                reportStatus = p.reportStatus,
                requestDate = p.requestDate,
                reportPath = p.reportPath,
            }).ToListAsync();

            if (allReports.Count == 0)
            {
                return new ReportReturnData
                {
                    response = false,
                    message = "Listelenecek rapor bulunmamaktadır.",
                    data = null
                };
            }
            else
            {
                return new ReportReturnData
                {
                    response = true,
                    message = "Raporlar listelenmektedir.",
                    data = allReports
                };
            }
        }

        public async Task<ReportReturnData> GetReportDetail(Guid reportUuId)
        {
            var result = await reportContext.reportTable.Where(p => p.uuId == reportUuId).Select(p => new ReportDetailData
            {
                reportData = new ReportData()
                {
                    uuId = p.uuId,
                    reportStatus = p.reportStatus,
                    requestDate = p.requestDate,
                    reportPath=p.reportPath,
                },
                detailData = p.reportDetails.Select(d => new DetailData
                {
                    uuId = d.uuId,
                    location = d.location,
                    personCount = d.personCount,
                    phoneNumberCount = d.phoneNumberCount
                }).ToList()
            }).FirstOrDefaultAsync();

            if (result == null)
            {
                return new ReportReturnData
                {
                    response = true,
                    message = "Rapor listelenememektedir.",
                    data = null
                };
            }
            else
            {
                return new ReportReturnData
                {
                    response = true,
                    message = "Rapor listelenmektedir.",
                    data = result
                };
            }
        }

        public async Task<ReportReturnData> CreateReportDetail(Guid reportUuId)
        {
            var report = await reportContext.reportTable.Where(x => x.uuId == reportUuId).FirstOrDefaultAsync();

            if (report == null)
            {
                return new ReportReturnData
                {
                    response = true,
                    message = "Rapor kaydı bulunamadı.",
                    data = null
                };
            }

            var client = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{reportSettings.contactApiUrl}/Person/ContactInformations");
            var response = await client.SendAsync(request);

            var responseStream = await response.Content.ReadAsStringAsync();
            var contactInformations = JsonConvert.DeserializeObject<IEnumerable<ReportContactInformationData>>(JsonConvert.SerializeObject((JsonConvert.DeserializeObject<ReportReturnData>(responseStream)).data));

            var statisticsReport = contactInformations.Where(x => x.informationType == 2).Select(x => x.informationContent).Distinct().Select(x => new ReportDetail
            {
                reportUuId = reportUuId,
                location = x,
                personCount = contactInformations.Where(y => y.informationType == 2 && y.informationContent == x).Count(),
                phoneNumberCount = contactInformations.Where(y => y.informationType == 0 && contactInformations.Where(y => y.informationType == 2 && y.informationContent == x).Select(x => x.personUuId).Contains(y.personUuId)).Count(),
        });
            var reportPath = Directory.GetCurrentDirectory() + "\\Reports\\PHONE.BOOK.REPORT." + Guid.NewGuid() + ".csv";
            await GenerateReportDocument(statisticsReport, reportPath);

            report.reportStatus = ReportStatusType.completed;
            report.reportPath = reportPath;
            await reportContext.reportDetailTable.AddRangeAsync(statisticsReport);
            await reportContext.SaveChangesAsync();
            return new ReportReturnData
            {
                response = true,
                message = "Rapor tamamlandı. Oluşturulan rapor dokümanı: "+reportPath,
                data = report
            };
        }

        public async Task CreateRabbitMQPublisher(ReportRequestData reportRequestData,ReportSettings reportSettings)
        {
            var conn = reportSettings.rabbitMqConsumer;

            var createDocumentQueue = "create_document_queue";
            var documentCreateExchange = "document_create_exchange";

            ConnectionFactory connectionFactory = new()
            {
                Uri = new Uri(conn)
            };

            var connection = connectionFactory.CreateConnection();

            var channel = connection.CreateModel();
            channel.ExchangeDeclare(documentCreateExchange, "direct");

            channel.QueueDeclare(createDocumentQueue, false, false, false);
            channel.QueueBind(createDocumentQueue, documentCreateExchange, createDocumentQueue);

            channel.BasicPublish(documentCreateExchange, createDocumentQueue, null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(reportRequestData)));

        }

        public async Task GenerateReportDocument(IEnumerable<ReportDetail> reportDetailList, string rPath)
        {
            var builder = new StringBuilder();
            builder.AppendLine("ReportId;Location;PersonCount;PhoneNumberCount");

            var folder = Directory.GetCurrentDirectory() + "\\Reports";
          
            if (!Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder + "\\Reports");

            foreach (var reportRecord in reportDetailList)
            {
                builder.AppendLine($"{reportRecord.reportUuId};{reportRecord.location};{reportRecord.personCount};{reportRecord.phoneNumberCount}");
                
            }

            File.WriteAllText(rPath, builder.ToString());

            builder.Clear();
        }
    }
}
