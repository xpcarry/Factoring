using Factoring.Domain.Contracts.Queries;
using Factoring.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Factoring.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContractsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetContracts([FromQuery] ContractStatusEnum status)
        {
            var result = await _mediator.Send(new GetContractsQuery(status));

            return Ok(result);
        }
    }
}
