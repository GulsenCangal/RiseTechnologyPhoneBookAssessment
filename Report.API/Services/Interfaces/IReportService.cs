using Report.API.Constants;
using Report.API.Data;

namespace Report.API.Services.Interfaces
{
    public interface IReportService
    {
        Task<ReportReturnData> CreateNewReport();
        Task<ReportReturnData> CreateReportDetail(Guid reportUuId);
        Task<ReportReturnData> GetAllReports();
        Task<ReportReturnData> GetReportDetail(Guid reportUuId);
        Task CreateRabbitMQPublisher(ReportRequestData reportRequestData, ReportSettings reportSettings);
    }
}
