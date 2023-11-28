using FakeItEasy;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace CryptoTracker_backend.Test.ControllerTests.AlertControllerTest
{
    public class DeleteAlertTest: AlertsControllerTests
    {

        [Fact]
        public async Task DeleteAlert_ReturnsUnauthorizedObjectResult_WhenUserTokenIsInvalid()
        {
            //arrange
            A.CallTo(() => _fakeTokenService.IsUserToken(A<int>._, A<ClaimsPrincipal>._)).Returns(true);

            var result = await _alertsController.DeletAlert(1, "ethereum");

            //asert
            var unauthorizedObjectResult = Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task DeleteAlert_ReturnsOkObjectResult_WhenUserTokenIsValid()
        {
            // arrage
            A.CallTo(() => _fakeTokenService.IsUserToken(A<int>._, A<ClaimsPrincipal>._)).Returns(false);

            var r = new MockAlerts().GetAlerts();
            ActionResult mock = new OkObjectResult(r);

            A.CallTo(() => _fakeAlertService.DeleteAlert(A<int>._, A<string>._))
                .Returns(Task.FromResult(mock));

            //act
            var result = await _alertsController.DeletAlert(9, "ethereum");


            var jsonResult = Assert.IsType<OkObjectResult>(result);

        }
    }
}
