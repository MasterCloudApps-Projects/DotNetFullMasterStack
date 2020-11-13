using Customer.API.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Customer.API.Infrastructure
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        public DbSet<Models.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public class CustomerContextDesignFactory : IDesignTimeDbContextFactory<CustomerContext>
        {
            public CustomerContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>()
                    .UseSqlServer("Server=tcp:127.0.0.1,1433;Initial Catalog=restaurant.Services.Customer;Integrated Security=true");

                return new CustomerContext(optionsBuilder.Options);
            }
        }
    }
}