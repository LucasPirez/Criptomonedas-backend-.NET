using CryptoTracker_backend.Controllers;
using CryptoTracker_backend.entities;
using CryptoTracker_backend.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CryptoTracker_backend.DTOs;

namespace CryptoTracker_backend.Test.ControllerTests.AlertControllerTest
{
    public class AlertsControllerTests
    {
        protected AlertsController _alertsController;
        protected ITokenService _fakeTokenService;
        protected IAlertService _fakeAlertService;

        public AlertsControllerTests()
        {
            // Dependencies
            _fakeTokenService = A.Fake<ITokenService>();
            _fakeAlertService = A.Fake<IAlertService>();

            //SUT
            _alertsController = new AlertsController(_fakeTokenService, _fakeAlertService);
        }
     
    }
}

