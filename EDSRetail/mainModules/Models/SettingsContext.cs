using Microsoft.EntityFrameworkCore;
using System.IO;

namespace mainModules
{
    public class SettingsContext : DbContext
    {
        string DBDirectory = databaseAPI.utilities.GetDatabasePath();


        public DbSet<databaseAPI.Models.Settings> Settings { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                $"Data Source={Path.Combine(DBDirectory, "edsretail.db")}");
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
