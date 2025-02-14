using System.ComponentModel.DataAnnotations;

namespace Factoring.Infrastructure.Entities
{
    public class Bank
    {
        public int BankId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(11)]
        public string SWIFT { get; set; }

        public ICollection<FactoringContract> Contracts { get; set; }
    }
}
