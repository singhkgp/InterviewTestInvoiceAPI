using InterviewTestInvoiceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace InterviewTestInvoiceAPI.Repositories
{
    public class TestInvoiceRepository : ITestInvoice
    {
        private readonly TestDbContext _context;

        public TestInvoiceRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TestInvoice>> GetInvoicesAsync()
        {
            return await _context.TestInvoice.ToListAsync();
        }

        public async Task<TestInvoice> GetInvoiceByIdAsync(Guid? id)
        {
            return await _context.TestInvoice.FindAsync(id) ?? new();
        }

        public async Task CreateInvoiceAsync(TestInvoice invoice)
        {
            _context.TestInvoice.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInvoiceAsync(TestInvoice invoice)
        {
            _context.TestInvoice.Update(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoiceAsync(Guid id)
        {
            var invoice = await _context.TestInvoice.FindAsync(id);
            if (invoice != null)
            {
                _context.TestInvoice.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }
    }
}
