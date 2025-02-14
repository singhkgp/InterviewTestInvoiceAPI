using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using InterviewTestInvoiceAPI.Models;

namespace InterviewTestInvoiceAPI
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

        public DbSet<TestInvoice> TestInvoice { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestInvoice>()
           .ToContainer("Products") // Specify the container name
           .HasPartitionKey(p => p.Id); // Specify the partition key

            modelBuilder.Entity<TestInvoice>().Property(e => e.CustomerName).HasMaxLength(200);
            base.OnModelCreating(modelBuilder);
        }

    }
}
