using Contact.API.Enums;

namespace Report.API.Data
{
    public class ReportContactInformationData
    {
        public Guid UUID { get; set; }
        public InformationType InformationType { get; set; }
        public string InformationContent { get; set; }
        public Guid PersonId { get; set; }
    }
}
