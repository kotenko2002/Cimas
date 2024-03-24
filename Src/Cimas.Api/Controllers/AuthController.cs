using Cimas.Application.Features.Auth.Commands.Login;
using Cimas.Application.Features.Auth.Commands.RefreshTokens;
using Cimas.Api.Contracts.Auth;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Cimas.Api.Common.Extensions;
using Cimas.Domain.Entities.Users;
using Cimas.Application.Features.Auth.Commands.RegisterNonOwner;
using Cimas.Application.Features.Auth.Commands.RegisterOwner;

namespace Cimas.Api.Controllers
{
    [Route("auth")]
    public class AuthController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor
        ) : base(mediator)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("register/owner")]
        public async Task<IActionResult> Register(RegisterOwnerRequest request)
        {
            var command = request.Adapt<RegisterOwnerCommand>();
            ErrorOr<Success> registerOwnerResult = await _mediator.Send(command);

            return registerOwnerResult.Match(
                NoContent,
                Problem
            );
        }

        [HttpPost("register/nonowner"), Authorize(Roles = Roles.Owner)]
        public async Task<IActionResult> CreateUser(RegisterNonOwnerRequest request)
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            var command = (userIdResult.Value, request).Adapt<RegisterNonOwnerCommand>();
            ErrorOr<Success> registerNonOwnerResult = await _mediator.Send(command);

            return registerNonOwnerResult.Match(
                NoContent,
                Problem
            );
        }

        [HttpPatch("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = request.Adapt<LoginCommand>();

            var loginResult = await _mediator.Send(command);

            return loginResult.Match(
                Ok,
                Problem
            );
        }

        [HttpPatch("refresh-tokens")]
        public async Task<IActionResult> RefreshTokens(RefreshTokensRequest request)
        {
            var command = request.Adapt<RefreshTokensCommand>();

            var refreshTokensResult = await _mediator.Send(command);

            return refreshTokensResult.Match(
                Ok,
                Problem
            );
        }
    }
}
