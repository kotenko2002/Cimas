using Cimas.Api.Common.Extensions;
using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cimas.Api.Controllers
{
    [Route("dev/roles")]
    [ApiController]
    public class RolesController : BaseController
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomUserManager _userManager;

        public RolesController(
            IMediator mediator,
            RoleManager<IdentityRole<Guid>> roleManager,
            IHttpContextAccessor httpContextAccessor,
            ICustomUserManager userManager) : base(mediator)
        {
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        [HttpPost("AddRolesToDb")]
        public async Task AddRolesToDb() 
        {
            foreach (var role in Roles.GetRoles())
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }

        [HttpPost("AddUserAllRoles"), Authorize]
        public async Task<IActionResult> AddUserAllRoles()
        {
            ErrorOr<Guid> userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            User user = await _userManager.FindByIdAsync(userIdResult.Value.ToString());

            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            string[] rolesToAdd = Roles.GetRoles().Except(userRoles).ToArray();
            if (rolesToAdd.Any())
            {
                IdentityResult result = await _userManager.AddToRolesAsync(user, rolesToAdd);

                if (!result.Succeeded)
                {
                    return Problem("An error occurred while adding roles to the user.");
                }
            }

            return Ok("Roles added successfully.");
        }
    }
}
