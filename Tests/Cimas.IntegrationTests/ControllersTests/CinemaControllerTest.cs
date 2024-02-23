using Cimas.Contracts.Cinemas;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Text;

namespace Cimas.IntegrationTests.ControllersTests
{
    public class CinemaControllerTest : BaseControllerTest
    {
        private const string _baseUrl = "cinemas";

        [Test]
        public Task CinemaController_CreateCinema_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange
                await GenerateTokenAndSetAsHeader(username: owner1UserName);

                var requestModel = new CreateCinemaRequest("Cinema #created", "created street");
                var content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

                // Act
                var response = await client.PostAsync($"{_baseUrl}", content);
                
                var cinemas = await GetResponseContent<GetCinemaResponse>(response);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(cinemas.Id.ToString(), Is.Not.EqualTo("00000000-0000-0000-0000-000000000000"));
            });
        }

        [Test]
        public Task CinemaController_GetCinemaById_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange
                await GenerateTokenAndSetAsHeader(username: owner1UserName);

                // Act
                var response = await client.GetAsync($"{_baseUrl}/{cinema1Id}");
                
                var cinemas = await GetResponseContent<GetCinemaResponse>(response);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            });
        }

        [Test]
        public Task CinemaController_GetAllCinemas_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange
                await GenerateTokenAndSetAsHeader(username: owner1UserName);

                // Act
                var response = await client.GetAsync($"{_baseUrl}");
                
                var cinemas = await GetResponseContent<List<GetCinemaResponse>>(response);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(cinemas.Count, Is.EqualTo(2));
            });
        }

        [Test]
        public Task CinemaController_UpdateCinema_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange
                await GenerateTokenAndSetAsHeader(username: owner1UserName);

                var requestModel = new UpdateCinemaRequest("Cinema #updated", "updated street");
                var content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

                // Act
                var response = await client.PutAsync($"{_baseUrl}/{cinema1Id}", content);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            });
        }

        [Test]
        public Task CinemaController_DeleteCinema_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange
                await GenerateTokenAndSetAsHeader(username: owner1UserName);

                // Act
                var response = await client.DeleteAsync($"{_baseUrl}/{cinema1Id}");

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            });
        }
    }
}
