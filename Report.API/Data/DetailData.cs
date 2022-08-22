namespace Report.API.Data
{
    public class DetailData
    {
        public Guid uuId { get; set; }
        public string location { get; set; }
        public int personCount { get; set; }
        public int phoneNumberCount { get; set; }
        public Guid reportUuId { get; set; }
    }
}
