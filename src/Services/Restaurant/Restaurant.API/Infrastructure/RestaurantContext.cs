using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Restaurant.API.Infrastructure.EntityConfigurations;

namespace Restaurant.API.Infrastructure
{
    public class RestaurantContext : DbContext
    {

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

            base.OnModelCreating(modelBuilder);
        }

        public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<RestaurantContext>
        {
            public RestaurantContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<RestaurantContext>()
                    .UseSqlServer("Server=tcp:127.0.0.1,1433;Initial Catalog=restaurant.Services.Restaurant;Integrated Security=true");

                return new RestaurantContext(optionsBuilder.Options);
            }
        }
    }
}