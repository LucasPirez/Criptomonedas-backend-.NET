using CryptoTracker_backend.Contexts;
using CryptoTracker_backend.DTOs;
using CryptoTracker_backend.entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using CryptoTracker_backend.Entities;

namespace CryptoTracker_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlertsController :ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlertsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("obtainAll")]
        public async Task<ActionResult> Obte()
        {

            var alert = await _context.Alerts.ToListAsync();
            if (alert == null)
            {
                return NotFound();
            }
            return new JsonResult(alert);

        }



        [HttpGet("obtainAlerts")]
        public async Task<ActionResult> ObtenerAlertsN(int idUser)
        {
            /*    var alerts = await _context.Alerts.Where(a => a.UserDataId == idUser).Select(a => new
                            {
                                a.CoinName,
                                a.DateCreate,
                                a.MinPrice,
                                a.MaxPrice,
                            }).ToListAsync();*/
            var alerts = await _context.UserDatas.Include(u => u.Alerts).FirstOrDefaultAsync(u=> u.Id == idUser);

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
             
            };


            if (alerts == null)
            {
                return NotFound();
            }
            return new JsonResult(alerts, jsonSerializerOptions);

        }

   

        [HttpPost("agregar")]
        public async Task<ActionResult> Post(int userDataId, AlertCreacionDTO alertCreacion)
        { 
            var alert = new Alert()
            {
                CoinName = alertCreacion.CoinName,
                MaxPrice = alertCreacion.MaxPrice,
                MinPrice = alertCreacion.MinPrice,
                DateCreate = alertCreacion.DateCreate,
                UserDataId = userDataId

            };
       
            _context.Alerts.Add(alert);

             await _context.SaveChangesAsync();

             return Ok();
            }
    }
}
