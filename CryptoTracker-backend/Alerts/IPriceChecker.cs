using CryptoTracker_backend.entities;
using CryptoTracker_backend.Entities;
using CryptoTracker_backend.Models;
using System.Text.Json;

namespace CryptoTracker_backend.Alerts
{
    public interface IPriceChecker
    {

        public List<Alert> IsNotify(User user, JsonElement priceCoins);
         
    }
}
