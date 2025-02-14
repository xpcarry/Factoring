using Factoring.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factoring.Infrastructure.Entities
{
    public class FactoringContract
    {
        public FactoringContract()
        {
            Invoices = new HashSet<Invoice>();
        }

        [Key]
        public Guid ContractId { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime IssueDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [ForeignKey("Bank")]
        public int BankId { get; set; }

        [Required]
        [StringLength(100)]
        public string DebtorName { get; set; }

        [Required]
        public ContractStatusEnum Status { get; set; } = ContractStatusEnum.Active;

        public Bank Bank { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
