namespace Factoring.Domain.Invoices.Dtos
{
    public class InvoiceResponse
    {
        public Guid InvoiceId { get; init; }

        public string InvoiceNumber { get; init; }

        public DateTime DueDate { get; init; }

        public decimal Amount { get; init; }
    }
}
