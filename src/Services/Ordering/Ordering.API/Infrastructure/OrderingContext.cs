using Kitchen.API.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Ordering.API.Models;

namespace Ordering.API.Infrastructure
{
    public class OrderingContext : DbContext
    {
        public OrderingContext(DbContextOptions<OrderingContext> options) : base(options)
        {
        }

        public OrderingContext()
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

            //base.OnModelCreating(modelBuilder);
        }
    }
}