using System.ComponentModel;

namespace Contact.API.Enums
{
    public enum InformationType
    {
        [Description("Telefon Numarası")]
        phoneNumber,
        [Description("E-Mail Adresi")]
        emailAddress,
        [Description("Konum Bilgisi")]
        location

    }
}

