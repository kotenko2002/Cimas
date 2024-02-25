using Cimas.Api.Common.Extensions;
using Cimas.Application.Features.Cinemas.Commands.DeleteCinema;
using Cimas.Application.Features.Halls.Commands.CreateHall;
using Cimas.Application.Features.Halls.Commands.DeleteHall;
using Cimas.Application.Features.Halls.Commands.UpdateHallSeats;
using Cimas.Application.Features.Halls.Queries.GetHallsByCinemaId;
using Cimas.Contracts.Halls;
using Cimas.Domain.Entities.Halls;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cimas.Api.Controllers
{
    [Route("halls"), Authorize(Roles = Roles.Owner)]
    public class HallController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HallController(
          IMediator mediator,
          IHttpContextAccessor httpContextAccessor
        ) : base(mediator)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHall(CreateHallRequest request)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            var command = (userIdResult.Value, request).Adapt<CreateHallCommand>();
            ErrorOr<Success> createCinemaResult = await _mediator.Send(command);

            return createCinemaResult.Match(
                NoContent,
                Problem
            );
        }

        [HttpGet("{cinemaId}")]
        public async Task<IActionResult> GetHallsByCinemaId(Guid cinemaId)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            var command = new GetHallsByCinemaIdQuery(userIdResult.Value, cinemaId);
            ErrorOr<List<Hall>> getHallsResult = await _mediator.Send(command);

            return getHallsResult.Match(
                halls => Ok(halls.Adapt< List<GetHallResponse>>()),
                Problem
            );
        }

        [HttpPatch("{hallId}")]
        public async Task<IActionResult> UpdateHallSeats(Guid hallId, UpdateHallSeatsRequst requst)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            var command = (userIdResult.Value, hallId, requst).Adapt<UpdateHallSeatsCommand>();
            ErrorOr<Success> updateHallSeatsResult = await _mediator.Send(command);

            return updateHallSeatsResult.Match(
                NoContent,
                Problem
            );
        }

        [HttpDelete("{hallId}")]
        public async Task<IActionResult> DeleteHall(Guid hallId)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            var command = new DeleteHallCommand(userIdResult.Value, hallId);
            ErrorOr<Success> deleteCinemaResult = await _mediator.Send(command);

            return deleteCinemaResult.Match(
                NoContent,
                Problem
            );
        }
    }
}
