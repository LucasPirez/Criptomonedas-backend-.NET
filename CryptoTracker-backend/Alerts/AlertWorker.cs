using CryptoTracker_backend.entities;
using CryptoTracker_backend.Services;
using System.Text.Json;

namespace CryptoTracker_backend.Alerts
{
    public class AlertWorker : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly IPriceChecker _priceChecker;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private List<Alert> listToDelete = new List<Alert>();
        public AlertWorker(
            IHttpClientFactory httpClientFactory,
            IPriceChecker priceChecker,
            IServiceScopeFactory  serviceScopeFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CryptoAPI");
            _priceChecker = priceChecker;
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using(var alertScope = _serviceScopeFactory.CreateScope())
            {
                while (!stoppingToken.IsCancellationRequested)
                {  
                    var alertService =  alertScope
                                        .ServiceProvider
                                        .GetRequiredService<IAlertService>();

                    if (alertService != null)
                    {
                        var queryCoins = await  alertService.GetCoinsInAlerts();
                        var rootResponse = await GetPriceCoins(queryCoins);
                        var UsersWithAlerts = await alertService.GetUsersWithAlerts();

                        foreach (var user in UsersWithAlerts)
                        {
                            listToDelete.AddRange(_priceChecker.IsNotify(user, rootResponse));
                        }

                         await  alertService.DeleteListOfAlerts(listToDelete);
                    }
                    Console.WriteLine("parand222o");

                    await Task.Delay(50000, stoppingToken);
                }
            }
        }

        private async Task<JsonElement> GetPriceCoins(string queryCoins)
        {
            string url = BuildPricesQueryUrl(queryCoins);
            var httpResponse = await SendHttpRequest(url);

            JsonElement rootResponse = ParseJsonResponse(httpResponse);

            return rootResponse;
        }

        private string BuildPricesQueryUrl(string Coins)
        {
          return $"/api/v3/simple/price?ids={Coins}&vs_currencies=usd";

        }

        private async Task<string> SendHttpRequest(string url)
        {
            try {  
                return   await _httpClient.GetStringAsync(url);

            }catch
            {
                return "[{}]";
            }

        }

        private JsonElement ParseJsonResponse(string response)
        { 
            JsonDocument ParseResponse = JsonDocument.Parse(response);
            return ParseResponse.RootElement;
        }
        
    }
}
