using Contact.API.Enums;

namespace Report.API.Data
{
    public class ReportContactInformationData
    {
        public Guid uuId { get; set; }
        public int informationType { get; set; }
        public string informationContent { get; set; }
        public Guid personUuId { get; set; }

       
    }
}
