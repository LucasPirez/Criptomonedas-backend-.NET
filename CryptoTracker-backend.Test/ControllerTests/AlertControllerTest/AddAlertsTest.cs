

using CryptoTracker_backend.DTOs;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CryptoTracker_backend.Test.ControllerTests.AlertControllerTest
{
    public class AddAlertsTest : AlertsControllerTests
    {
     private   AlertCreacionDTO alert = new AlertCreacionDTO() { 
        
         CoinName = "nueva",
         DateCreate = DateTime.Now,
         MinPrice = 0,
          MaxPrice = 120
         
     };

        [Fact]
        public  async Task AddAlert_ReturnsUnauthorizedObjectResult_WhenUserTokenIsInvalid()
        {
            //arrange
            A.CallTo(()=> _fakeTokenService.IsUserToken(A<int>._,A<ClaimsPrincipal>._)).Returns(true);

            var result = await _alertsController.AddAlert(1, alert ); 

            //asert
            var unauthorizedObjectResult = Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task AddAlert_ReturnsOkObjectResult_WhenUserTokenIsValid()
        {
            // arrage
            A.CallTo(() => _fakeTokenService.IsUserToken(A<int>._, A<ClaimsPrincipal>._)).Returns(false);

            var r = new MockAlerts().GetAlerts();
            ActionResult e = new OkObjectResult(r);

            A.CallTo(() => _fakeAlertService.AddAlert(A<int>._, A<AlertCreacionDTO>._))
                .Returns(Task.FromResult(e));

            //act
            var result = await _alertsController.AddAlert(9, alert);

      
            var jsonResult = Assert.IsType<OkObjectResult>(result);

        }
    }
}
