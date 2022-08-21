using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Report.API.Models.Entity
{
    [Table("ReportDetails")]
    public class ReportDetail
    {
        [Key]
        [Required]
        public Guid uuId { get; set; }
        public string location { get; set; }
        public int personCount { get; set; }
        public int phoneNumberCount { get; set; }
        public Guid reportUuId { get; set; }
    }
}
