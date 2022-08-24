using Report.API.Enums;
using Report.API.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report.API.Models.Entity
{
    [Table("Reports")]
    public class Report
    {
        [Key]
        [Required]
        public Guid uuId { get; set; }
        public DateTime requestDate { get; set; }
        public ReportStatusType reportStatus { get; set; }
        public string reportPath { get; set; }
        public virtual List<ReportDetail> reportDetails { get; set; }
    }
}
