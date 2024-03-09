﻿using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Sessions.Commands.CreateSession
{
    public record CreateSessionCommand(
        Guid UserId,
        Guid HallId,
        Guid FilmId,
        DateTime StartTime
    ) : IRequest<ErrorOr<Success>>;
}
