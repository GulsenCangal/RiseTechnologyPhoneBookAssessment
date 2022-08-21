using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact.API.Models.Entity
{
    [Table("Persons")]
    public class Person
    {
        [Key]
        [Required]
        public Guid uuId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string company { get; set; }
        public DateTime creationDate { get; set; } = DateTime.UtcNow;
        public virtual List<ContactInformation> contactInformation { get; set; }
    }
}
