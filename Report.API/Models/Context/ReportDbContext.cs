using Microsoft.EntityFrameworkCore;
using Report.API.Models.Entity;

namespace Report.API.Models.Context
{
    public class ReportDbContext: DbContext
    {
        public ReportDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet <Report.API.Models.Entity.Report> reportTable { get; set; }
        public DbSet<ReportDetail> reportDetailTable { get; set; }
    }
}
