

using CryptoTracker_backend.Controllers;
using CryptoTracker_backend.DTOs;
using CryptoTracker_backend.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CryptoTracker_backend.Test.ControllerTests.AlertControllerTest
{
    public class ObtainUserAlertsTest: AlertsControllerTests
    {



        [Fact]
        public async Task ObtenerUserAlerts_ReturnsUnauthorizedObjectResult_WhenTokenIsInvalid()
        {
            // Arrange
            A.CallTo(() => _fakeTokenService.IsUserToken(A<int>._, A<ClaimsPrincipal>._)).Returns(true);

            // Act
            var result = await _alertsController.ObtenerUserAlerts(1);

            // Assert
            var unauthorizedObjectResult = Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task ObtenerUserAlerts_ReturnsNotFoundObjectResult_WhenAlertsAreNull()
        {
            // Arrange

            A.CallTo(() => _fakeTokenService.IsUserToken(A<int>._, A<ClaimsPrincipal>._)).Returns(false);


            A.CallTo(() => _fakeAlertService.GetUserAlerts(A<int>._)).Returns(Task.FromResult<List<AlertResponseDTO>>(null));

            // Act
            var result = await _alertsController.ObtenerUserAlerts(1);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);

        }

        [Fact]
        public async Task ObtenerAlertsN_ReturnsJsonResult_WhenAlertsAreNotNull()
        {
            // Arrange

            A.CallTo(() => _fakeTokenService.IsUserToken(A<int>._, A<ClaimsPrincipal>._)).Returns(false);


            var fakeAlerts =  new MockAlerts().GetAlerts();
            A.CallTo(() => _fakeAlertService.GetUserAlerts(A<int>._)).Returns(Task.FromResult(fakeAlerts));

            // Act
            var result = await _alertsController.ObtenerUserAlerts(1);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal(fakeAlerts, jsonResult.Value);
        }

    }
}
