using BlankApp1.DataBaseLay.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.DataBaseLay
{
    public class AppDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = DataFile.db");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tranzaction> Tranzactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RegularTranzaction> RegularTranzactions { get; set; }
    }
}
