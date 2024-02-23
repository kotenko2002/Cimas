using Cimas.Application.Features.Companies.Commands.CreateCompany;
using Cimas.Contracts.Companies;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cimas.Api.Controllers
{
    [Route("companies")]
    public class CompanyController : BaseController
    {
        public CompanyController(
            IMediator mediator,
            IMapper mapper
        ) : base(mediator, mapper) {}

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyRequest request)
        {
            var command = _mapper.Map<CreateCompanyCommand>(request);

            var createCompanyResult = await _mediator.Send(command);

            return createCompanyResult.Match(
                Ok,
                Problem
            );
        }
    }
}
