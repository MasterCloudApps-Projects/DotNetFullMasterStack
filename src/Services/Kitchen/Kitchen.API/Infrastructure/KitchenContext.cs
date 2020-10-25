using Microsoft.EntityFrameworkCore;

namespace Kitchen.API.Infrastructure
{
    public class KitchenContext : DbContext
    {
        public KitchenContext()
        {
        }

        public KitchenContext(DbContextOptions<KitchenContext> options) : base(options)
        {
        }

        public DbSet<Models.Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}