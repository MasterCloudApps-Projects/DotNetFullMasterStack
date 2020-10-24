using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.API.Infrastructure.EntityConfigurations
{
 

    public class RestaurantEntityTypeConfiguration : IEntityTypeConfiguration<Models.Restaurant>
    {
        public void Configure(EntityTypeBuilder<Models.Restaurant> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(m => m.Id)
              .UseHiLo("restaurant_hilo")
              .IsRequired();
        }
    }
}
