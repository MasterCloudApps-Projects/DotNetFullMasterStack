using Microsoft.EntityFrameworkCore;
using Restaurant.API.Infrastructure.EntityConfigurations;

namespace Restaurant.API.Infrastructure
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext()
        {
        }

        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {
        }

        public DbSet<Models.MenuItem> MenuItems { get; set; }
        public DbSet<Models.Menu> Menus { get; set; }
        public DbSet<Models.Restaurant> Restaurants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MenuEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MenuItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantEntityTypeConfiguration());

            //base.OnModelCreating(modelBuilder);
        }
    }
}