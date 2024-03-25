using NUnit.Framework;
using System.Net;

namespace Cimas.IntegrationTests.ControllersTests
{
    public class WorkdayControllerTest : BaseControllerTest
    {
        private const string _baseUrl = "workdays";

        #region StartWorkday
        [Test]
        public Task WorkdayController_StartWorkday_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange
                await GenerateTokenAndSetAsHeader(username: worker1UserName);

                // Act
                var response = await client.PostAsync($"{_baseUrl}/start/{cinema1Id}", null);

                //// Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            });
        }
        #endregion

        #region GetCurrentWorkday

        /* TODO: add tow tests
         *   - 200 when workday exists
         *   - 204when workday does not exist
         */
        #endregion

        #region FinishWorkday
        #endregion
    }
}
