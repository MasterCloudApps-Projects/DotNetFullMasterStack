using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.API.Infrastructure.EntityConfigurations
{
 

    public class KitchenEntityTypeConfiguration : IEntityTypeConfiguration<Models.Ticket>
    {
        public void Configure(EntityTypeBuilder<Models.Ticket> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(m => m.Id)
              .UseHiLo("ticket_hilo")
              .IsRequired();
        }
    }
}
