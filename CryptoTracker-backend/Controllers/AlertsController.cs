using CryptoTracker_backend.Contexts;
using CryptoTracker_backend.DTOs;
using CryptoTracker_backend.entities;
using CryptoTracker_backend.Services;
using CryptoTracker_backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace CryptoTracker_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
 
    public class AlertsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

   
        public AlertsController(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }


        [HttpGet("obtainAll"), Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> ObteinAll()
        {

            var alert = await _context.Alerts.ToListAsync();

            if (alert == null)
            {
                return new NotFoundResult();
            }

            return new JsonResult(alert);
        }



        [HttpGet("obtainAlerts"), Authorize]
      
        public async Task<ActionResult> ObtenerAlertsN(int idUser)
        {
            if (_tokenService.IsUserToken(idUser,User))
                return new UnauthorizedObjectResult(new { message = "token is invalid for this user" });

            var alerts = await _context.Alerts.Where(a => a.UserId == idUser).Select(a => new
            {
                a.CoinName,
                a.DateCreate,
                a.MinPrice,
                a.MaxPrice
            }).ToListAsync();


            if (alerts == null)
            {
                return  new NotFoundObjectResult(new
                {
                    message = "No alert was found for that cryptocurrency, please try again"
                });
                }
            
            return new JsonResult(alerts);

        }

        [HttpPost("Add"), Authorize]
        public async Task<ActionResult> Post(int userDataId, AlertCreacionDTO alertCreacion)
        {
           
            if (_tokenService.IsUserToken(userDataId,User))
                return new UnauthorizedObjectResult(new { message = "token is invalid for this user" });

            var alert = new Alert()
            {
                CoinName = alertCreacion.CoinName,
                MaxPrice = alertCreacion.MaxPrice,
                MinPrice = alertCreacion.MinPrice,
                DateCreate = alertCreacion.DateCreate,
                UserId = userDataId

            };
       
            _context.Alerts.Add(alert);

             await _context.SaveChangesAsync();

             return Ok();
            }

        [HttpPut("Edit"), Authorize]
        public async Task<ActionResult> EditAlert(int userDataId, AlertCreacionDTO alertCreacion)
        {
            if (_tokenService.IsUserToken(userDataId, User))
                return new UnauthorizedObjectResult(new { message = "The token is invalid for that user" });


            var alertToEdit = await _context.Alerts.Where(
                a => a.UserId == userDataId && a.CoinName == alertCreacion.CoinName)
                 .FirstOrDefaultAsync();

            if (alertToEdit == null)
            {
                return new NotFoundObjectResult(new {message = "No alert was found for that cryptocurrency, please try again " });
            }

            alertToEdit.MinPrice = alertCreacion.MinPrice;
            alertToEdit.MaxPrice = alertCreacion.MaxPrice;
            alertToEdit.DateCreate = new DateTime();

            _context.Entry(alertToEdit).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("Delete"), Authorize]
        public async Task<ActionResult> DeletAlert(int userDataId, string CoinName)
        {
            if (_tokenService.IsUserToken(userDataId,User))
                return new UnauthorizedObjectResult(new { message = "The token is invalid for that user" });


            var alertToEdit = await _context.Alerts.Where(
                a => a.UserId == userDataId && a.CoinName == CoinName)
                 .FirstOrDefaultAsync();

            if (alertToEdit == null)
            {
                return new NotFoundObjectResult(new { message = "No alert was found for that cryptocurrency, please try again " });
            }

            _context.Alerts.Remove(alertToEdit);

            await _context.SaveChangesAsync();

            return Ok();
        }

       

    }
}


/*eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvdXJpIjoiMjUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiZXhwIjoxNjk3NjY4MTAzfQ.7SHR1yapdfZUZFh07mqgMaaNBxoOuIyEcoGaFi2LgNI*/