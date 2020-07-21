using Customer.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.API.Infrastructure
{
    public class CustomerContext : DbContext
    {

        public CustomerContext()
        {

        }

        public CustomerContext (DbContextOptions<CustomerContext> options) : base(options) { }

        public DbSet<Models.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
