using Contact.API.Data;
using Contact.API.Models.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Report.API.Data;
using Report.API.Entities;
using Report.API.Enums;
using Report.API.Models.Context;
using Report.API.Models.Entity;
using Report.API.Services.Interfaces;
using System.Net.Http;

namespace Report.API.Services
{
    public class ReportService:IReportService
    {
        ReportDbContext reportContext;
        public ReportService(ReportDbContext context)
        {
            reportContext = context;
        }

        public async Task<ReportReturnData> CreateNewReport()
        {
            var report = new Report.API.Entities.Report()
            {
                requestDate = DateTime.UtcNow,
                reportStatus = ReportStatusType.preparing,
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
            int reportCount = reportContext.reportTable.Count();

            if (reportCount == 0)
            {
                return new ReportReturnData
                {
                    response = false,
                    message = "Listelenecek kayıt bulunmamaktadır.",
                    data = null
                };
            }

            var allReports = await reportContext.reportTable.Select(p => new ReportData
            {
                uuId = p.uuId,
                reportStatus = p.reportStatus,
                requestDate = p.requestDate,
            }).ToListAsync();

            if (allReports == null)
            {
                return new ReportReturnData
                {
                    response = false,
                    message = "Raporlar listelenirken hata oluştu.",
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
    }
}
