using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MiniERP.Api.Models;
using MiniERP.Models; // This assumes you’ll create models like Product.cs

namespace MiniERP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } 
        public DbSet<Customer> Customers { get; set; }
    }
}
