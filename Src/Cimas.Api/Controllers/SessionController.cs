using Cimas.Api.Common.Extensions;
using Cimas.Api.Contracts.Sessions;
using Cimas.Application.Features.Sessions.Commands.CreateSession;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cimas.Api.Controllers
{
    [Route("sessions"), Authorize] // (Roles = Roles.Worker)
    public class SessionController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionController(
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor
        ) : base(mediator)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession(CreateSessionRequest request)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            var command = (userIdResult.Value, request).Adapt<CreateSessionCommand>();
            ErrorOr<Success> createSessionResult = await _mediator.Send(command);

            return createSessionResult.Match(
                NoContent,
                Problem
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetSessionsByRange()
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            // TODO: impliment
            
            return Ok();
        }

        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetSessionById(Guid sessionId)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            // TODO: impliment

            return Ok();
        }

        [HttpDelete("{sessionId}")]
        public async Task<IActionResult> DeleteSession(Guid sessionId)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            // TODO: impliment

            return Ok();
        }
    }
}
