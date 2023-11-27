using CryptoTracker_backend.DTOs;
using CryptoTracker_backend.Services;
using CryptoTracker_backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CryptoTracker_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
 
    public class AlertsController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAlertService _alertService;

        public AlertsController( ITokenService tokenService,IAlertService alertService)
        {
            _tokenService = tokenService;
            _alertService = alertService;
        }


        [HttpGet("obtainAll"), Authorize(Roles = Roles.Admin)]

        public async Task<ActionResult> ObtainAll()
        {
            var alert = await _alertService.GetAllAlerts();

            if (alert == null)
            {
                return new NotFoundResult();
            }
            return new JsonResult(alert);
        }



        [HttpGet("obtainAlerts"), Authorize]
        public async Task<ActionResult> ObtenerUserAlerts(int idUser)
        {
            if (_tokenService.IsUserToken(idUser,User))
                return new UnauthorizedObjectResult(new ApiErrorDTO("token is invalid for this user","Unauthorized", HttpStatusCode.Unauthorized));

            var alerts = await _alertService.GetUserAlerts(idUser);

            if (alerts == null)
            {
                return new NotFoundObjectResult(new ApiErrorDTO("token is invalid for this user",
                    "Not Found", HttpStatusCode.NotFound));
            }

            return new JsonResult(alerts);
        }


        [HttpPost("Add"), Authorize]
        public async Task<ActionResult> Post(int userDataId, AlertCreacionDTO alertCreacion)
        {
           
            if (_tokenService.IsUserToken(userDataId,User))
                return new UnauthorizedObjectResult(new ApiErrorDTO("token is invalid for this user", "Unauthorized", HttpStatusCode.Unauthorized));

            return await _alertService.AddAlert(userDataId, alertCreacion);
        }


        [HttpPut("Edit"), Authorize]
        public async Task<ActionResult> EditAlert(int userDataId, AlertCreacionDTO alertCreacion)
        {
            if (_tokenService.IsUserToken(userDataId, User))
                return new UnauthorizedObjectResult(new ApiErrorDTO("token is invalid for this user", "Unauthorized", HttpStatusCode.Unauthorized));

            return await _alertService.EditAlert(userDataId, alertCreacion);
        }


        [HttpDelete("Delete"), Authorize]
        public async Task<ActionResult> DeletAlert(int userDataId, string CoinName)
        {
            if (_tokenService.IsUserToken(userDataId,User))
                return new UnauthorizedObjectResult(new ApiErrorDTO("token is invalid for this user", "Unauthorized", HttpStatusCode.Unauthorized));

            return await _alertService.DeleteAlert(userDataId, CoinName);
        }
    }
}
