using Factoring.Domain.Contracts;
using Factoring.Domain.Enums;
using Factoring.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Factoring.Infrastructure.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly FactoringDbContext _dbContext;

        public ContractRepository(FactoringDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<FactoringContract>> GetContractByStatusAsync(ContractStatusEnum? status)
        {
            var query = _dbContext.FactoringContracts
                .Include(c => c.Bank)
                .Include(c => c.Invoices)
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(c => c.Status == status);
            }

            return await query.ToListAsync();
        }
    }
}
