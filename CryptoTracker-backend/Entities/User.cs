using CryptoTracker_backend.entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CryptoTracker_backend.Entities
{
    public class User {

        [Key]
        public int UserDataId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
     
        public List<Alert> Alerts { get; set; } = new List<Alert>();
    }
}
