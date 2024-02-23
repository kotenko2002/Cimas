using Cimas.Api;
using Cimas.Infrastructure.Common;
using Cimas.Infrastructure.Auth;
using Cimas.Domain.Users;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cimas.Infrastructure.Identity;
using System.Data;
using Cimas.Domain.Companies;
using Cimas.Domain.Cinemas;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Cimas.IntegrationTests.ControllersTests
{
    public class BaseControllerTest
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;

        #region HardcodedInfo
        protected readonly string owner1UserName = "owner1";
        protected readonly string owner2UserName = "owner2";
        protected readonly string worker1UserName = "worker1";
        protected readonly string worker2UserName = "worker2";
        protected readonly string reviewer1UserName = "reviewer1";
        protected readonly string reviewer2UserName = "reviewer2";

        protected readonly Guid cinema1Id = Guid.NewGuid();
        #endregion

        public async Task PerformTest(Func<HttpClient, Task> testFunc, Action<IServiceCollection> configureServices = null)
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    SetUpInMemoryDb(services);
                    configureServices?.Invoke(services);
                });
            });
            await SeedData();
            _client = _factory.CreateClient();

            await testFunc(_client);

            _client.Dispose();
            _factory.Dispose();
        }

        public async Task GenerateTokenAndSetAsHeader(string username)
        {
            using var scope = _factory.Services.CreateScope();
            var config = scope.ServiceProvider.GetRequiredService<IOptions<JwtConfig>>().Value;
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            User user = await userManager.FindByNameAsync(username);

            var authClaims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            IList<string> userRoles = await userManager.GetRolesAsync(user);

            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret));

            var accessToken = new JwtSecurityToken(
                issuer: config.ValidIssuer,
                audience: config.ValidAudience,
                expires: DateTime.Now.AddMinutes(config.TokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            string token = new JwtSecurityTokenHandler().WriteToken(accessToken);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        private void SetUpInMemoryDb(IServiceCollection services)
        {
            string databaseName = Guid.NewGuid().ToString();

            var dbContextDescriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<CimasDbContext>));
            services.Remove(dbContextDescriptor);
            services.AddDbContext<CimasDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName);
            });
        }

        private async Task SeedData()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CimasDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<CustomUserManager>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            foreach (var role in Roles.GetRoles())
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));

            Company company1 = new() { Id = Guid.NewGuid(), Name = "Company #1" };
            Company company2 = new() { Id = Guid.NewGuid(), Name = "Company #2" };
            await context.Companies.AddRangeAsync(company1, company2);

            User owner1 = await AddUser(userManager, company1, owner1UserName, Roles.Owner);
            User owner2 = await AddUser(userManager, company2, owner2UserName, Roles.Owner);
            User worker1 = await AddUser(userManager, company1, worker1UserName, Roles.Worker);
            User worker2 = await AddUser(userManager, company2, worker2UserName, Roles.Worker);
            User reviewer1 = await AddUser(userManager, company1, reviewer1UserName, Roles.Reviewer);
            User reviewer2 = await AddUser(userManager, company2, reviewer2UserName, Roles.Reviewer);

            Cinema cinema1 = new() { Id = cinema1Id, Company = company1, Name = "Cinema #1", Adress = "1 street" };
            Cinema cinema2 = new() { Id = Guid.NewGuid(), Company = company2, Name = "Cinema #2", Adress = "2 street" };
            Cinema cinema3 = new() { Id = Guid.NewGuid(), Company = company1, Name = "Cinema #3", Adress = "3 street" };
            await context.Cinemas.AddRangeAsync(cinema1, cinema2, cinema3);

            await context.SaveChangesAsync();
        }

        private async Task<User> AddUser(
            CustomUserManager userManager,
            Company company,
            string username,
            string role)
        {
            var user = new User()
            {
                Company = company,
                UserName = username,
                RefreshToken = "refresh_token",
                RefreshTokenExpiryTime = DateTime.Now.AddDays(1)
            };

            await userManager.CreateAsync(user, "Qwerty123!");
            await userManager.AddToRoleAsync(user, role);

            return user;
        }
    }
}
