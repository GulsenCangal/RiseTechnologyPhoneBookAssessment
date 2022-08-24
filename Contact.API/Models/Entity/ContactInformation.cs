using Contact.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact.API.Models.Entity
{
    [Table("ContactInformations")]
    public class ContactInformation
    {
        [Key]
        [Required]
        public Guid uuId { get; set; }
        public InformationType informationType { get; set; }
        public string informationContent { get; set; }
        public DateTime creationDate { get; set; } = DateTime.UtcNow;
        public Guid personUuId { get; set; }
    }
}
