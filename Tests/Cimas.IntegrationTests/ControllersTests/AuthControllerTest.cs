using Cimas.Api.Contracts.Auth;
using Cimas.Domain.Entities.Users;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Text;

namespace Cimas.IntegrationTests.ControllersTests
{
    public class AuthControllerTest : BaseControllerTest
    {
        private const string _baseUrl = "auth";

        #region RegisterOwner
        [Test]
        public Task AuthController_RegisterOwner_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange

                var requestModel = new RegisterOwnerRequest(
                    "Company #created",
                    "FirstName #created",
                    "LastName #created",
                    "testUserName41",
                    "Qwerty123!"
                );
                var content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

                // Act
                var response = await client.PostAsync($"{_baseUrl}/register/owner", content);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            });
        }
        #endregion

        #region RegisterNonOwner
        [TestCase(Roles.Worker)]
        [TestCase(Roles.Reviewer)]
        public Task AuthController_RegisterNonOwner_ShouldReturnOk(string role)
        {
            return PerformTest(async (client) =>
            {
                // Arrange
                await GenerateTokenAndSetAsHeader(username: owner1UserName);

                var requestModel = new RegisterNonOwnerRequest(
                    "FirstName #created",
                    "LastName #created",
                    "testUserName41",
                    "Qwerty123!",
                    role
                );
                var content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

                // Act
                var response = await client.PostAsync($"{_baseUrl}/register/nonowner", content);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            });
        }
        #endregion

        #region Login
        [Test]
        public Task AuthController_Login_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange
                await GenerateTokenAndSetAsHeader(username: owner1UserName);

                var requestModel = new LoginRequest(
                    owner1UserName,
                    "Qwerty123!"
                );
                var content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

                // Act
                var response = await client.PatchAsync($"{_baseUrl}/login", content);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            });
        }
        #endregion

        #region RefreshTokens
        [Test]
        public Task AuthController_RefreshTokens_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange
                string accessToken = await GenerateTokenAndSetAsHeader(username: owner1UserName, setTikenAsHeader: false);

                var requestModel = new RefreshTokensRequest(
                    accessToken,
                    owner1FisrtRefreshToken
                );
                var content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

                // Act
                var response = await client.PatchAsync($"{_baseUrl}/refresh-tokens", content);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            });
        }
        #endregion
    }
}
