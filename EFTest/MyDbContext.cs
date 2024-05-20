using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    public class MyDbContext : DbContext
    {
        public DbSet<MyEntity> Entities { get; set; }
    }
}
