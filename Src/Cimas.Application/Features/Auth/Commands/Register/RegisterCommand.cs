using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(
        Guid CompanyId,
        string Username,
        string Password,
        string Role
    ) : IRequest<ErrorOr<Success>>;
}
