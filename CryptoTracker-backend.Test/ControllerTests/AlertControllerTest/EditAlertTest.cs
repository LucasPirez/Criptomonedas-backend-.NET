using CryptoTracker_backend.DTOs;
using CryptoTracker_backend.entities;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker_backend.Test.ControllerTests.AlertControllerTest
{
    public class EditAlertTest: AlertsControllerTests
    {

        private AlertCreacionDTO alert = new AlertCreacionDTO()
        {

            CoinName = "nueva",
            DateCreate = DateTime.Now,
            MinPrice = 0,
            MaxPrice = 120

        };

        [Fact]
        public async Task EditAlert_ReturnsUnauthorizedObjectResult_WhenUserTokenIsInvalid()
        {
            //arrange
            A.CallTo(() => _fakeTokenService.IsUserToken(A<int>._, A<ClaimsPrincipal>._)).Returns(true);

            var result = await _alertsController.EditAlert(1, alert);

            //asert
            var unauthorizedObjectResult = Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task EditAlert_ReturnsOkObjectResult_WhenUserTokenIsValid()
        {
            // arrage
            A.CallTo(() => _fakeTokenService.IsUserToken(A<int>._, A<ClaimsPrincipal>._)).Returns(false);

            var r = new MockAlerts().GetAlerts();
            ActionResult mock = new OkObjectResult(r);

            A.CallTo(() => _fakeAlertService.EditAlert(A<int>._, A<AlertCreacionDTO>._))
                .Returns(Task.FromResult(mock));

            //act
            var result = await _alertsController.EditAlert(9, alert);


            var jsonResult = Assert.IsType<OkObjectResult>(result);

        }
    }
}
