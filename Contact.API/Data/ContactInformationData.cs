using Contact.API.Enums;

namespace Contact.API.Data
{
    public class ContactInformationData
    {
        public Guid uuId { get; set; }
        public InformationType informationType { get; set; }
        public string informationContent { get; set; }
        public Guid personUuId { get; set; }
    }
}
