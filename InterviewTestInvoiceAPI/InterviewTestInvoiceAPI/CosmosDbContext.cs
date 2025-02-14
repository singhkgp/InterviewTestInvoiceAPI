using InterviewTestInvoiceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InterviewTestInvoiceAPI
{
    public class CosmosDbContext : DbContext
    {
        //public CosmosDbContext(DbContextOptions<CosmosDbContext> options) : base(options) { }

        private string accountEndPoint = "https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private string accountKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private string dbName = "InterviewInvoiceTest";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(accountEndPoint, accountKey, dbName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestInvoice>()
            
           .ToContainer("Products") // Specify the container name
           .HasPartitionKey(p => p.Id); // Specify the partition key

            //modelBuilder.Entity<TestInvoice>().Property(e => e.YourColumn).HasMaxLength(4000);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TestInvoice> TestInvoice { get; set; }
    }
}
