using Contact.API.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Contact.API.Models.Context
{
    public class ContactDbContext:DbContext
    {
        public ContactDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public virtual DbSet<Person> personTable { get; set; }
        public virtual DbSet<ContactInformation> contactInformationTable { get; set; }
    }
}
