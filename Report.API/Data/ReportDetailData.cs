namespace Report.API.Data
{
    public class ReportDetailData
    {
        public ReportData reportData { get; set; }
        public List<DetailData> detailData { get; set; } = new();
    }
}
