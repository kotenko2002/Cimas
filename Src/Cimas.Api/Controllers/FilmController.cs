using Cimas.Api.Common.Extensions;
using Cimas.Api.Contracts.Films;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cimas.Api.Controllers
{
    [Route("films"), Authorize(Roles = Roles.Worker)]
    public class FilmController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FilmController(
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor
        ) : base(mediator)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("{cinemaId}")]
        public async Task<IActionResult> CreateFilm(Guid cinemaId, CreateFilmRequest request)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            return Ok();
        }

        [HttpGet("{cinemaId}")]
        public async Task<IActionResult> GetFilmssByCinemaId(Guid cinemaId)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            return Ok();
        }
        
        [HttpDelete("{filmId}")]
        public async Task<IActionResult> DeleteFilm(Guid filmId)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            return Ok();
        }
    }
}
