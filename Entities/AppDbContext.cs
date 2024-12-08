using Microsoft.EntityFrameworkCore;
using PaggingSample.Models;

namespace PaggingSample.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
