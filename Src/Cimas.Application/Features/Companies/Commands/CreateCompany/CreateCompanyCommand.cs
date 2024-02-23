using Cimas.Domain.Companies;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Companies.Commands.CreateCompany
{
    public record CreateCompanyCommand(string Name) : IRequest<ErrorOr<Company>>;
}
