using Microsoft.EntityFrameworkCore;
using System.IO;

namespace mainModules
{
    public class AuditContext : DbContext
    {
        string DBDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"db");


        public DbSet<databaseAPI.Models.SystemAudit> systemAudits { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                $"Data Source={Path.Combine(DBDirectory, "edsretail.db")}");
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
