namespace Contact.API.Data
{
    public class PersonDetailData
    {
        public PersonData person { get; set; }
        public List<ContactInformationData> contactInformation { get; set; } = new();
    }
}
