using Factoring.Domain.Enums;
using Factoring.Infrastructure.Entities;

namespace Factoring.Domain.Contracts
{
    public interface IContractRepository
    {
        Task<ICollection<FactoringContract>> GetContractByStatusAsync(ContractStatusEnum? status);
    }
}