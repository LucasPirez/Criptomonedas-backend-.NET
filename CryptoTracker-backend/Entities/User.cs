using CryptoTracker_backend.entities;

namespace CryptoTracker_backend.Entities
{
    public class User { 
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Age { get; set; }
        public string Email { get; set; }

        public List<Alert> Alerts { get; set; } = new List<Alert>();
    }
}
