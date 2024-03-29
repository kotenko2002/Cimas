﻿using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Auth.Commands.RegisterNonOwner
{
    public record RegisterNonOwnerCommand(
        Guid OwnerUserId,
        string FisrtName,
        string LastName,
        string Username,
        string Password,
        string Role
    ) : IRequest<ErrorOr<Success>>;
}
