using Kitchen.API.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Ordering.API.Models;

namespace Ordering.API.Infrastructure
{
    public class OrderingContext : DbContext
    {
        public OrderingContext(DbContextOptions<OrderingContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentInfo> PaymentsInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderLineEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentInfoEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<OrderingContext>
        {
            public OrderingContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<OrderingContext>()
                    .UseSqlServer("Server=tcp:127.0.0.1,1433;Initial Catalog=restaurant.Services.Ordering;Integrated Security=true");

                return new OrderingContext(optionsBuilder.Options);
            }
        }
    }
}