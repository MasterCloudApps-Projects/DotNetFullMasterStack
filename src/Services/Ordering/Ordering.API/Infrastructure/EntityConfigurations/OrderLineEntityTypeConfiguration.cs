using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ordering.API.Models;

namespace Kitchen.API.Infrastructure.EntityConfigurations
{
 

    public class OrderLineEntityTypeConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(m => m.Id)
              .UseHiLo("order_line_hilo")
              .IsRequired();
        }
    }
}
