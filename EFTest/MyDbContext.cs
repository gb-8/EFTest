using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    public class MyDbContext : DbContext
    {
        public DbSet<MyEntity> Entities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Initial Catalog=EFTest;Integrated Security=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyEntity>().Property("name");
        }

    }
}
