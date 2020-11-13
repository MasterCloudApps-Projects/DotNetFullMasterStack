using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.API.Infrastructure.EntityConfigurations
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Models.Customer>
    {
        public void Configure(EntityTypeBuilder<Models.Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(o => o.Id);

            builder.Property(m => m.Id)
              .UseHiLo("customer_hilo")
              .IsRequired();
        }
    }
}
