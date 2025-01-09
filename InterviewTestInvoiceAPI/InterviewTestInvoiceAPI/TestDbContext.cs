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
    }
}
