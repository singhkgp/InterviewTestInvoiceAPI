using InterviewTestInvoiceAPI.Models;

namespace InterviewTestInvoiceAPI.Repositories
{
    public interface ITestInvoice
    {
        Task<IEnumerable<TestInvoice>> GetInvoicesAsync();
        Task<TestInvoice> GetInvoiceByIdAsync(Guid? id);
        Task CreateInvoiceAsync(TestInvoice invoice);
        Task UpdateInvoiceAsync(TestInvoice invoice);
        Task DeleteInvoiceAsync(Guid id);
    }
}
