using CustomerApi.Application.DTO;
using CustomerApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Infrastructure
{
    public class CustomerDb : DbContext
    {

        public CustomerDb(DbContextOptions options) : base(options) { }
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;

        //Not needed for now, EF auto-detects this 
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Order>()
        //        .HasOne(o => o.Customer)
        //        .WithMany(c => c.Orders)
        //        .HasForeignKey(o => o.CustomerId);
        //}

    }
}
