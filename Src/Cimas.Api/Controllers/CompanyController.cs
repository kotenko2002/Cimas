using Cimas.Application.Features.Companies.Commands.CreateCompany;
using Cimas.Api.Contracts.Companies;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cimas.Api.Controllers
{
    [Route("companies")]
    public class CompanyController : BaseController
    {
        public CompanyController(
            IMediator mediator
        ) : base(mediator) {}

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyRequest request)
        {
            var command = request.Adapt<CreateCompanyCommand>();

            var createCompanyResult = await _mediator.Send(command);

            return createCompanyResult.Match(
                company => Ok(company.Adapt<CompanyResponse>()),
                Problem
            );
        }
    }
}
