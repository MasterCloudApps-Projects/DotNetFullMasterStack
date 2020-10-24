using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.API.Infrastructure.EntityConfigurations
{
 

    public class MenuEntityTypeConfiguration : IEntityTypeConfiguration<Models.Menu>
    {
        public void Configure(EntityTypeBuilder<Models.Menu> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(m => m.Id)
              .UseHiLo("menu_hilo")
              .IsRequired();
        }
    }
}
