namespace CryptoTracker_backend.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserDataId { get; set; }
        public UserData UserData { get; set; } = null!;
    }
}
