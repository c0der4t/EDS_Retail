using Microsoft.EntityFrameworkCore;
using System.IO;

namespace mainModules
{
    public class DebtorContext : DbContext
    {
        string DBDirectory = databaseAPI.utilities.GetDatabasePath();


        public DbSet<databaseAPI.Models.Debtor> Debtors { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                $"Data Source={Path.Combine(DBDirectory, "edsretail.db")}");
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
