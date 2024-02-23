using Cimas.Application.Interfaces;
using Cimas.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cimas.Infrastructure.Identity
{
    public class CustomUserManager : UserManager<User>, ICustomUserManager
    {
        public CustomUserManager(
            IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger)
        : base(store,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger)
        { }

        public override async Task<User> FindByIdAsync(string userId)
        {
            return await Users
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        }
    }
}
