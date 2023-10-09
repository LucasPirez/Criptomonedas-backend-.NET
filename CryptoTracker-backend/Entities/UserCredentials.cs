namespace CryptoTracker_backend.Entities
{
    public class UserCredentials
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
