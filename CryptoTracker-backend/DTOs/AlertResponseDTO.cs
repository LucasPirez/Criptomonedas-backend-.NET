namespace CryptoTracker_backend.DTOs
{
    public class AlertResponseDTO
    {
        public string CoinId { get; set; } = string.Empty;
       public DateTime DateCreate { get; set; }
       public double  MinPrice { get; set; }
       public double MaxPrice { get; set; }
    }
}
