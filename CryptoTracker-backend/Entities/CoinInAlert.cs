using CryptoTracker_backend.entities;
using System.ComponentModel.DataAnnotations;

namespace CryptoTracker_backend.Entities
{
    public class CoinInAlert
    {
        [Key]
        public string CoinId { get; set; }

        public List<Alert> AlertWithThisCoin { get; set; } = new List<Alert>();

    }
}
