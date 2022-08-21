using Microsoft.EntityFrameworkCore;
using Report.API.Entities;
using Report.API.Models.Entity;

namespace Report.API.Models.Context
{
    public class ReportDbContext: DbContext
    {
        public ReportDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet <Report.API.Entities.Report> reportTable { get; set; }
        public DbSet<ReportDetail> reportDetailTable { get; set; }
    }
}
