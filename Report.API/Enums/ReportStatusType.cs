using System.ComponentModel;

namespace Report.API.Enums
{
    public enum ReportStatusType
    {
        [Description("Hazırlanıyor")]
        preparing,
        [Description("Tamamlandı")]
        completed
    }
}

