using Cimas.Application.Features.Auth.Commands.Login;
using Cimas.Application.Features.Auth.Commands.RefreshTokens;
using Cimas.Application.Features.Auth.Commands.Register;
using Cimas.Contracts.Auth;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cimas.Api.Controllers
{
    [Route("auth")]
    public class AuthController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(
            IMediator mediator,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
        ) : base(mediator, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /*
        * TODO: 
        * 
        * 1) impl another register endpoint for adding new workers,
        * check userId from jwt token and is user Owner
        * 
        * 2) edit current register endpoint logic. Make sure that
        * company doesnt have any user
        */
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);

            var loginResult = await _mediator.Send(command);

            return loginResult.Match(
                res => Ok(),
                Problem
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = _mapper.Map<LoginCommand>(request);

            var loginResult = await _mediator.Send(command);

            return loginResult.Match(
                Ok,
                Problem
            );
        }

        [HttpPost("refresh-tokens")]
        public async Task<IActionResult> RefreshTokens(RefreshTokensRequest request)
        {
            var command = _mapper.Map<RefreshTokensCommand>(request);

            var refreshTokensResult = await _mediator.Send(command);

            return refreshTokensResult.Match(
                Ok,
                Problem
            );
        }
    }
}
