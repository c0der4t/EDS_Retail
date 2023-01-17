using Microsoft.EntityFrameworkCore;
using System.IO;

namespace mainModules
{
    public class UserContext : DbContext
    {
        string DBDirectory = databaseAPI.utilities.GetDatabasePath();


        public DbSet<databaseAPI.Models.User> Users { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                $"Data Source={Path.Combine(DBDirectory, "edsretail.db")}");
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
