using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MiniERP.Models; // This assumes you’ll create models like Product.cs

namespace MiniERP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } // We'll create Product.cs next
    }
}
