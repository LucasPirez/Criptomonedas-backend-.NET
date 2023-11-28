using CryptoTracker_backend.entities;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;


namespace CryptoTracker_backend.Test.ControllerTests.AlertControllerTest
{
    public class ObtainAllTest: AlertsControllerTests
    {

        [Fact]
        public async Task ObtainAll_ReturnsNotFoundResult_WhenAlertsAreNull()
        {
            // Arrange

            A.CallTo(() => _fakeAlertService.GetAllAlerts()).Returns(Task.FromResult<List<Alert>>(null));

            // Act
            var result = await _alertsController.ObtainAll();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ObteinAll_ReturnsJsonResult_WhenAlertsAreNotNull()
        {
            // Arrange

            var fakeAlerts = new List<Alert> { new Alert() {

                CoinId = "ethereum",
                DateCreate = new DateTime(),
                MaxPrice= 2000,
                MinPrice = 1000,

            },new Alert() {

                CoinId = "bitcoin",
                DateCreate = new DateTime(),
                MaxPrice= 40000,
                MinPrice = 1000

            }
            };
            A.CallTo(() => _fakeAlertService.GetAllAlerts()).Returns(Task.FromResult(fakeAlerts));

            // Act
            var result = await _alertsController.ObtainAll();

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal(fakeAlerts, jsonResult.Value);
        }
    }
}
