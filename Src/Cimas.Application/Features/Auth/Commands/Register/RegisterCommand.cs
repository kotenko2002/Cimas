using Cimas.Domain.Entities.Companies;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(
        Company Company,
        string Username,
        string Password,
        string Role
    ) : IRequest<ErrorOr<Success>>;
}
