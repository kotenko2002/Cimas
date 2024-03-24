using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Auth.Commands.RegisterOwner
{
    public record RegisterOwnerCommand(
        string CompanyName,
        string FisrtName,
        string LastName,
        string Username,
        string Password
    ) : IRequest<ErrorOr<Success>>;
}
