using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using MediatR;
using Cimas.Contracts.Cinemas;
using Cimas.Application.Features.Cinemas.Commands.CreateCinema;
using Cimas.Application.Features.Cinemas.Queries.GetCinemaById;
using Cimas.Application.Features.Cinemas.Queries.GetAllCinemas;
using Cimas.Application.Features.Cinemas.Commands.UpdateCinema;
using Cimas.Application.Features.Cinemas.Commands.DeleteCinema;
using Cimas.Api.Common.Extensions;
using Mapster;
using Cimas.Domain.Cinemas;
using ErrorOr;

namespace Cimas.Api.Controllers
{
    [Route("cinemas"), Authorize]
    public class CinemaController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CinemaController(
            IMediator mediator,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
        ) : base(mediator, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCinema(CreateCinemaRequest request)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            var command = (userIdResult.Value, request).Adapt<CreateCinemaCommand>();
            ErrorOr<Cinema> createCinemaResult = await _mediator.Send(command);

            return createCinemaResult.Match(
                cinema => Ok(cinema.Adapt<GetCinemaResponse>()),
                Problem
            );
        }

        [HttpGet("{cinemaId}")]
        public async Task<IActionResult> GetCinema(Guid cinemaId)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if(userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            var command = new GetCinemaQuery(userIdResult.Value, cinemaId);
            ErrorOr<Cinema> getCinemaResult = await _mediator.Send(command);

            return getCinemaResult.Match(
                cinema => Ok(cinema.Adapt<GetCinemaResponse>()),
                Problem
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCinemas()
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            var command = new GetAllCinemasQuery(userIdResult.Value);
            ErrorOr<List<Cinema>> getCinemasResult = await _mediator.Send(command);

            return getCinemasResult.Match(
                cinemas => Ok(cinemas.Adapt<List<GetCinemaResponse>>()),
                Problem
            );
        }

        [HttpPut("{cinemaId}")]
        public async Task<IActionResult> UpdateCinema(Guid cinemaId, UpdateCinemaRequest request)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            var command = (userIdResult.Value, cinemaId, request).Adapt<UpdateCinemaCommand>();
            ErrorOr<Success> updateCinemaResult = await _mediator.Send(command);

            return updateCinemaResult.Match(
                NoContent,
                Problem
            );
        }

        [HttpDelete("{cinemaId}")]
        public async Task<IActionResult> DeleteCinema(Guid cinemaId)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            var command = new DeleteCinemaCommand(userIdResult.Value, cinemaId);
            ErrorOr<Success> deleteCinemaResult = await _mediator.Send(command);

            return deleteCinemaResult.Match(
                NoContent,
                Problem
            );
        }
    }
}
