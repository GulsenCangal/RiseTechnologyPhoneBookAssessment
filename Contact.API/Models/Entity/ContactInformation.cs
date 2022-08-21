using Contact.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Contact.API.Models.Entity
{
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
