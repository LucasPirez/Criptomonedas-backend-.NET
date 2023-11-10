using CryptoTracker_backend.DTOs;
using CryptoTracker_backend.Models;

namespace CryptoTracker_backend.Services
{
    public interface INotificationService
    {
        void Notify(EmailDTO request);
    }
}
