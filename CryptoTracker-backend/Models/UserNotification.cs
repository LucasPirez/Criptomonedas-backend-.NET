using CryptoTracker_backend.DTOs;

namespace CryptoTracker_backend.Models
{
    public class UserNotification: UserEditDTO
    {
        public List<NotificationInfo> notifications { get; set; } = new List<NotificationInfo>();
    }
}
