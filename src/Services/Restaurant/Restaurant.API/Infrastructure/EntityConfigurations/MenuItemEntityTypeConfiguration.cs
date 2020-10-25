using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.API.Infrastructure.EntityConfigurations
{
 

    public class MenuItemEntityTypeConfiguration : IEntityTypeConfiguration<Models.MenuItem>
    {
        public void Configure(EntityTypeBuilder<Models.MenuItem> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(m => m.Id)
              .UseHiLo("menu_item_hilo")
              .IsRequired();
        }
    }
}
