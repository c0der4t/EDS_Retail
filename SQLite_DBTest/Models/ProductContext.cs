using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SQlLite_API.Models;

namespace SQLite_DBTest.Models
{
    internal class ProductContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=products.db");
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
