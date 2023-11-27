using CryptoTracker_backend.Controllers;
using CryptoTracker_backend.entities;
using CryptoTracker_backend.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CryptoTracker_backend.DTOs;

namespace CryptoTracker_backend.Test.ControllerTests
{
    public class AlertsControllerTests
    {
        private AlertsController _alertsController;
        private ITokenService _fakeTokenService;
        private IAlertService _fakeAlertService;

        public AlertsControllerTests()
        {
            // Dependencies
            _fakeTokenService = A.Fake<ITokenService>();
            _fakeAlertService = A.Fake<IAlertService>();

            //SUT
            _alertsController = new AlertsController(_fakeTokenService, _fakeAlertService);
        }

       

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

        [Fact] 
        public async Task ObtenerAlertsN_ReturnsUnauthorizedObjectResult_WhenTokenIsInvalid()
        {
            // Arrange
            A.CallTo(() => _fakeTokenService.IsUserToken(A<int>._, A<ClaimsPrincipal>._)).Returns(true);

            var controller = new AlertsController(_fakeTokenService, A.Fake<IAlertService>());

            // Act
            var result = await controller.ObtenerUserAlerts(1);

            // Assert
            var unauthorizedObjectResult = Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task ObtenerAlertsN_ReturnsNotFoundObjectResult_WhenAlertsAreNull()
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
        
            A.CallTo(() =>_fakeTokenService.IsUserToken(A<int>._, A<ClaimsPrincipal>._)).Returns(false);

     
            var fakeAlerts = new List<AlertResponseDTO> { /* Your fake alerts here */ };
            A.CallTo(() => _fakeAlertService.GetUserAlerts(A<int>._)).Returns(Task.FromResult(fakeAlerts));

            // Act
            var result = await _alertsController.ObtenerUserAlerts(1);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal(fakeAlerts, jsonResult.Value);
        }
    }
}

