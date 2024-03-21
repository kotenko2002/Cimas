using Cimas.Api;
using Cimas.Infrastructure.Common;
using Cimas.Infrastructure.Auth;
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
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Cimas.Domain.Entities.Users;
using Cimas.Domain.Entities.Companies;
using Cimas.Domain.Entities.Cinemas;
using Cimas.Domain.Entities.Halls;
using Cimas.Domain.Entities.Films;
using Cimas.Domain.Entities.Sessions;
using Cimas.Domain.Entities.Tickets;

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
        protected readonly Guid hall1Id = Guid.NewGuid();
        protected readonly Guid seat1Id = Guid.NewGuid();
        protected readonly Guid seat2Id = Guid.NewGuid();
        protected readonly Guid seat3Id = Guid.NewGuid();
        protected readonly Guid seat4Id = Guid.NewGuid();
        protected readonly Guid seat5Id = Guid.NewGuid();
        protected readonly Guid film1Id = Guid.NewGuid();
        protected readonly Guid film3Id = Guid.NewGuid();
        protected readonly Guid session1Id = Guid.NewGuid();
        protected readonly Guid session2Id = Guid.NewGuid();
        protected readonly Guid ticket1Id = Guid.NewGuid();
        protected readonly Guid ticket2Id = Guid.NewGuid();
        protected readonly Guid ticket3Id = Guid.NewGuid();
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
                new("userId", user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            IList<string> userRoles = await userManager.GetRolesAsync(user);

            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret));

            var accessToken = new JwtSecurityToken(
                issuer: config.ValidIssuer,
                audience: config.ValidAudience,
                expires: DateTime.UtcNow.AddMinutes(config.TokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            string token = new JwtSecurityTokenHandler().WriteToken(accessToken);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
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

            Hall hall1 = new() { Id = hall1Id, Cinema = cinema1, Name = "Hall #1" };
            Hall hall2 = new() { Id = Guid.NewGuid(), Cinema = cinema1, Name = "Hall #2", IsDeleted = true };
            Hall hall3 = new() { Id = Guid.NewGuid(), Cinema = cinema2, Name = "Hall #3" };
            await context.Halls.AddRangeAsync(hall1, hall2, hall3);

            HallSeat seat1 = new() { Id = seat1Id, Hall = hall1, Row = 0, Column = 0, Status = HallSeatStatus.Available };
            HallSeat seat2 = new() { Id = seat2Id, Hall = hall1, Row = 0, Column = 1, Status = HallSeatStatus.Available };
            HallSeat seat3 = new() { Id = seat3Id, Hall = hall1, Row = 1, Column = 0, Status = HallSeatStatus.Available };
            HallSeat seat4 = new() { Id = seat4Id, Hall = hall1, Row = 1, Column = 1, Status = HallSeatStatus.Available };
            HallSeat seat5 = new() { Id = seat5Id, Hall = hall2, Row = 0, Column = 0, Status = HallSeatStatus.Available };
            await context.Seats.AddRangeAsync(seat1, seat2, seat3, seat4, seat5);

            Film film1 = new() { Id = film1Id, Cinema = cinema1, Name = "Film #1", Duration = new TimeSpan(1, 0, 0) };
            Film film2 = new() { Id = Guid.NewGuid(), Cinema = cinema1, Name = "Film #2", Duration = new TimeSpan(1, 0, 0), IsDeleted = true };
            Film film3 = new() { Id = film3Id, Cinema = cinema2, Name = "Film #3", Duration = new TimeSpan(1, 0, 0), };
            await context.Films.AddRangeAsync(film1, film2, film3);

            Session session1 = new() { Id = session1Id, Film = film1, Hall = hall1, StartDateTime = DateTime.UtcNow };
            Session session2 = new() { Id = session2Id, Film = film2, Hall = hall1, StartDateTime = DateTime.UtcNow.AddMinutes(15) + film1.Duration };
            Session session3 = new() { Id = Guid.NewGuid(), Film = film3, Hall = hall3, StartDateTime = DateTime.UtcNow.AddDays(1) };
            Session session4 = new() { Id = Guid.NewGuid(), Film = film1, Hall = hall1, StartDateTime = DateTime.UtcNow.AddDays(2) };
            Session session5 = new() { Id = Guid.NewGuid(), Film = film2, Hall = hall1, StartDateTime = DateTime.UtcNow.AddDays(3) };
            Session session6 = new() { Id = Guid.NewGuid(), Film = film3, Hall = hall3, StartDateTime = DateTime.UtcNow.AddDays(4) };
            await context.Sessions.AddRangeAsync(session1, session2, session3, session4, session5, session6);

            Ticket ticket1 = new() { Id = ticket1Id, Seat = seat1, Session = session1, Status = TicketStatus.Booked, CreationTime = DateTime.UtcNow };
            Ticket ticket2 = new() { Id = ticket2Id, Seat = seat2, Session = session1, Status = TicketStatus.Sold, CreationTime = DateTime.UtcNow };
            Ticket ticket3 = new() { Id = ticket3Id, Seat = seat3, Session = session3, CreationTime = DateTime.UtcNow };
            await context.Tickets.AddRangeAsync(ticket1, ticket2, ticket3);

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
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1)
            };

            await userManager.CreateAsync(user, "Qwerty123!");
            await userManager.AddToRoleAsync(user, role);

            return user;
        }
    }
}
