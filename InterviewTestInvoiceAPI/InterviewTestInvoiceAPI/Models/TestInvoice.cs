using System.ComponentModel.DataAnnotations;

namespace InterviewTestInvoiceAPI.Models
{
    public class TestInvoice
    {
        public Guid Id { get; set; }
        public string? CustomerName { get; set; }
        public List<InvoiceItem>? Items { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
    }


    public class InvoiceItem
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }
}
