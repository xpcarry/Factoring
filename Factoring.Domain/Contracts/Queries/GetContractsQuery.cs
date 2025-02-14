using MediatR;
using Factoring.Domain.Contracts.Dtos;
using AutoMapper;
using Factoring.Domain.Enums;

namespace Factoring.Domain.Contracts.Queries
{

    public record GetContractsQuery(ContractStatusEnum? Status) : IRequest<ICollection<ContractResponse>>;

    public class GetContractsQueryHandler : IRequestHandler<GetContractsQuery, ICollection<ContractResponse>>
    {
        private readonly IContractRepository _repository;
        private readonly IMapper _mapper;

        public GetContractsQueryHandler(IContractRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<ContractResponse>> Handle(GetContractsQuery request, CancellationToken cancellationToken)
        {
            if (request.Status.HasValue && !Enum.IsDefined(typeof(ContractStatusEnum), request.Status.Value))
            {
                throw new ArgumentException("Invalid status value");
            }

            var contracts = await _repository.GetContractByStatusAsync(request.Status);

            return _mapper.Map<ICollection<ContractResponse>>(contracts);
        }
    }
}
