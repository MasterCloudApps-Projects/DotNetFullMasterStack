using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ordering.API.Models;

namespace Kitchen.API.Infrastructure.EntityConfigurations
{
 

    public class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(m => m.Id)
              .UseHiLo("address_hilo")
              .IsRequired();
        }
    }
}
