using Kitchen.API.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Kitchen.API.Infrastructure
{
    public class KitchenContext : DbContext
    {

        public KitchenContext(DbContextOptions<KitchenContext> options) : base(options)
        {
        }

        public DbSet<Models.Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new KitchenEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public class KitchenContextDesignFactory : IDesignTimeDbContextFactory<KitchenContext>
        {
            public KitchenContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<KitchenContext>()
                    .UseSqlServer("Server=tcp:127.0.0.1,1433;Initial Catalog=restaurant.Services.Kitchen;Integrated Security=true");

                return new KitchenContext(optionsBuilder.Options);
            }
        }
    }
}