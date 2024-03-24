﻿using Cimas.Api.Common.Extensions;
using Cimas.Api.Contracts.Users;
using Cimas.Application.Features.Auth.Commands.Register;
using Cimas.Application.Features.Users.Commands.FireUser;
using Cimas.Application.Features.Users.Commands.RegisterNonOwner;
using Cimas.Application.Features.Users.Commands.RegisterOwner;
using Cimas.Application.Features.Users.Queries.GetCompanyUsers;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cimas.Api.Controllers
{
    [Route("users"), Authorize(Roles = Roles.Owner)]
    public class UserController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor
        ) : base(mediator)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("register/owner"), AllowAnonymous]
        public async Task<IActionResult> Register(RegisterOwnerRequest request)
        {
            var command = request.Adapt<RegisterOwnerCommand>();
            ErrorOr<Success> registerOwnerResult = await _mediator.Send(command);

            return registerOwnerResult.Match(
                res => Ok(),
                Problem
            );
        }

        [HttpPost("register/nonowner")]
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

        //[HttpGet]
        //public async Task<IActionResult> GetCompanyUsers()
        //{
        //    ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
        //    if (userIdResult.IsError)
        //    {
        //        return Problem(userIdResult.Errors);
        //    }

        //    var query = new GetCompanyUsersQuery();
        //    ErrorOr<List<User>> getUsersResult = await _mediator.Send(query);

        //    return getUsersResult.Match(
        //        Ok,
        //        Problem
        //    );
        //}

        //[HttpDelete("{userId}")]
        //public async Task<IActionResult> FireUser(Guid userId)
        //{
        //    ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
        //    if (userIdResult.IsError)
        //    {
        //        return Problem(userIdResult.Errors);
        //    }

        //    var command = new FireUserCommand();
        //    ErrorOr<Success> deleteUserResult = await _mediator.Send(command);

        //    return deleteUserResult.Match(
        //        NoContent,
        //        Problem
        //    );
        //}
    }
}