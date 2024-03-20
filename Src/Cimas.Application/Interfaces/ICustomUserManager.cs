using Cimas.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Cimas.Application.Interfaces
{
    public interface ICustomUserManager
    {
        #region Base
        Task<User> FindByIdAsync(string userId);
        Task<bool> IsInRoleAsync(User user, string role);
        Task<User> FindByNameAsync(string userName);
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> AddToRoleAsync(User user, string role);
        Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<IdentityResult> UpdateAsync(User user);
        Task<IList<string>> GetRolesAsync(User user);
        #endregion
    }
}
