using Microsoft.EntityFrameworkCore;

namespace FlowersLab.Models
{
    public class FlowerShopContext : DbContext
    {
        public virtual DbSet<Flower> Flowers { get; set; }
        public virtual DbSet<Bouquet> Bouquets { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public FlowerShopContext(DbContextOptions<FlowerShopContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}