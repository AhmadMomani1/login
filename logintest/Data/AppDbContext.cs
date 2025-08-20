using logintest.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace logintest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("YourFallbackConnectionStringHere");
            }
        }

        public DbSet<User> Users { get; set; }
    }
}
