using Microsoft.EntityFrameworkCore;
using System.IO;

namespace mainModules.Models
{
    public class _dbContext : DbContext
    {
        string DBDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "db");


        public DbSet<databaseAPI.Models.Sale> Sales { get; set; }
        public DbSet<databaseAPI.Models.Stock> Stock { get; set; }
        public DbSet<databaseAPI.Models.User> Users { get; set; }
        public DbSet<databaseAPI.Models.Settings> Settings { get; set; }
        public DbSet<databaseAPI.Models.SystemAudit> SystemAudit { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                $"Data Source={Path.Combine(DBDirectory, "edsretail.db")}");
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
