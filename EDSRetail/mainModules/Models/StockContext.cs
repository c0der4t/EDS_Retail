using Microsoft.EntityFrameworkCore;
using System.IO;

namespace mainModules.Models
{
    public class StockContext : DbContext
    {
        string DBDirectory = databaseAPI.utilities.GetDatabasePath();


        public DbSet<databaseAPI.Models.Stock> Stock { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                $"Data Source={Path.Combine(DBDirectory, "edsretail.db")}");
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
