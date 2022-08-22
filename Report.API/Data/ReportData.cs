using Report.API.Enums;

namespace Report.API.Data
{
    public class ReportData
    {
        public Guid uuId { get; set; }
        public DateTime requestDate { get; set; }
        public ReportStatusType reportStatus { get; set; }
    }
}
