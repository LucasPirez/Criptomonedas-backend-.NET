using CryptoTracker_backend.Alerts;
using CryptoTracker_backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CryptoTracker_backend.Controllers
{
    [Route("Coin")]
    [ApiController]
    public class CoinsController
    {
        private readonly HttpClient _httpClient;
        private readonly IAlertService _alertService;
  

        public CoinsController(IHttpClientFactory httpClientFactory,IAlertService alertService)
        {
            _httpClient = httpClientFactory.CreateClient("CryptoAPI");
            _alertService = alertService;
          
        }

        [HttpGet("ps")]
        public async Task<ActionResult> GetPa1()
        {

            string url = "/api/v3/coins/bitcoin";

            var httpResponse = await _httpClient.GetStringAsync(url);

            JsonDocument response = JsonDocument.Parse(httpResponse);
            JsonElement rootResponse = response.RootElement;

            var UsersWithAlerts = await _alertService.GetUsersWithAlerts();

            foreach(var user in  UsersWithAlerts)
            {
            /*_priceChecker.IsNotify(user, rootResponse);*/
            }


            return new JsonResult(response);
         }

        [HttpGet("Users")]
        public async Task<object> getUsers()
            {
                    var response = await _alertService.GetUsersWithAlerts();

                    return  response;
            }

   

        }
}
