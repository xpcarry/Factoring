using Factoring.Domain.Invoices.Dtos;
using Factoring.Infrastructure;
namespace Factoring.Domain.Contracts.Dtos
{
    public class ContractResponse
    {
        public Guid ContractId { get; init; }

        public DateTime IssueDate { get; init; }

        public decimal Amount { get; init; }

        public string DebtorName { get; init; }

        public string Status { get; init; }

        public string BankName { get; init; }

        public string BankSWIFT { get; init; }

        public ICollection<InvoiceResponse> Invoices { get; init; }
    }
}
