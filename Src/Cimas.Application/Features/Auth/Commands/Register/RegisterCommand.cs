using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(
        Guid CompanyId,
        string FisrtName,
        string LastName,
        string Username,
        string Password,
        string Role
    ) : IRequest<ErrorOr<Success>>;
}
