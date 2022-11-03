using Microsoft.EntityFrameworkCore;
using System.IO;

namespace mainModules
{
    public class SalesContext : DbContext
    {
        string DBDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"db");


        public DbSet<databaseAPI.Models.Sale> Sales { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                $"Data Source={Path.Combine(DBDirectory, "sales.db")}");
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
