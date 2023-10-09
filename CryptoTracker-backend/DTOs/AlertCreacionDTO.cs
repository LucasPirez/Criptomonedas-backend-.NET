namespace CryptoTracker_backend.DTOs
{
    public class AlertCreacionDTO
    {

        public string CoinName { get; set; } = string.Empty;

        public double MinPrice { get; set; }

        public double MaxPrice { get; set; }

        public DateTime DateCreate { get; set; }
    }
}
