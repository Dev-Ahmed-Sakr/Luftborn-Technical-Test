using Luftborn_Technical_Test.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Luftborn_Technical_Test.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
