using Cimas.Api.Contracts.Films;
using Cimas.Api.Contracts.Halls;
using Cimas.Api.Contracts.Sessions;
using Cimas.Api.Contracts.Tickets;
using Cimas.Application.Features.Tickets.Commands.CreateTickets;
using Cimas.Domain.Entities.Tickets;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Cimas.IntegrationTests.ControllersTests
{
    public class TicketControllerTest : BaseControllerTest
    {
        private const string _baseUrl = "tickets";

        #region CreateTicket
        [Test]
        public Task TicketController_CreateTicketession_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange
                await GenerateTokenAndSetAsHeader(username: worker1UserName);
                var requestModel = new CreateTicketsRequest([
                    new (seat3Id, TicketStatus.Booked),
                    new (seat4Id, TicketStatus.Sold)
                ]);
                var content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

                // Act
                var response = await client.PostAsync($"{_baseUrl}/{session1Id}", content);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            });
        }
        #endregion

        #region DeleteTicket
        [Test]
        public Task TicketController_DeleteTicket_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange
                await GenerateTokenAndSetAsHeader(username: worker1UserName);

                var requestModel = new DeleteTicketsRequest(new List<Guid>() { ticket1Id, ticket2Id });
                var content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

                // Act
                var request = new HttpRequestMessage()
                {
                    Content = content,
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"{_baseUrl}", UriKind.Relative)
                };
                var response = await client.SendAsync(request);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            });
        }
        #endregion
    }
}
