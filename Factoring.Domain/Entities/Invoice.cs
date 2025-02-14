using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factoring.Infrastructure.Entities
{
    public class Invoice
    {
        [Key]
        public Guid InvoiceId { get; set; } = Guid.NewGuid();

        [ForeignKey("Contract")]
        public Guid ContractId { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public FactoringContract Contract { get; set; }
    }
}
