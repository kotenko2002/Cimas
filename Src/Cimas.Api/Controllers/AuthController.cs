using Cimas.Application.Features.Auth.Commands.Login;
using Cimas.Application.Features.Auth.Commands.RefreshTokens;
using Cimas.Api.Contracts.Auth;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cimas.Api.Controllers
{
    [Route("auth")]
    public class AuthController : BaseController
    {
        public AuthController(
            IMediator mediator
        ) : base(mediator) {}

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = request.Adapt<LoginCommand>();

            var loginResult = await _mediator.Send(command);

            return loginResult.Match(
                Ok,
                Problem
            );
        }

        [HttpPost("refresh-tokens")]
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
