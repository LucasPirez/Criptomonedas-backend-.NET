using CryptoTracker_backend.DTOs;
using CryptoTracker_backend.entities;
using CryptoTracker_backend.Entities;
using CryptoTracker_backend.Services;
using System.Text.Json;

namespace CryptoTracker_backend.Alerts
{
    public class PriceChecker: IPriceChecker
    {
        private readonly INotificationService _emailNotification;
        private EmailDTO Email = new EmailDTO();
        private List<Alert> alertsNotificated = new List<Alert>();
        public PriceChecker(INotificationService emailNotification) {
            _emailNotification = emailNotification;
        }
        public List<Alert> IsNotify(User user, JsonElement JsonResponse)
        {
            foreach (var item in user.Alerts)
            {
                if (JsonResponse.TryGetProperty(item.CoinId, out var coinName)){

                  var price = coinName.GetProperty("usd").GetDouble();

                    IsExceedLimits(price, item, user.Email);
                }
            }
            return alertsNotificated;
        }


        private  void IsExceedLimits(double price, Alert alert,string EmailUser)
            {
            if (price <= alert.MinPrice || price >= alert.MaxPrice)
            {
                CreateEmail(EmailUser, alert);
                SendEmail();
                alertsNotificated.Add(alert);
            }
        }


        private void CreateEmail(string Email, Alert alert)
        {
            this.Email = new()
            {
                Asunt = "Alerta de Precio",
                Content = $"El valor de {alert.CoinId} excedio uno de los limites establecidos",
                UserEmail = Email
            };
        }

        private void SendEmail()
        {
            _emailNotification.Notify(this.Email);
        }

    
    }
}
