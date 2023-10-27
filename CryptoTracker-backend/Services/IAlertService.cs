using CryptoTracker_backend.DTOs;
using CryptoTracker_backend.entities;
using CryptoTracker_backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoTracker_backend.Services
{
    public interface IAlertService
    {
        public Task<List<User>> GetUsersWithAlerts();
        public Task<string> GetCoinsInAlerts();
        public  Task<List<Alert>> GetAllAlerts();
        public  Task<List<AlertResponseDTO>> GetUserAlerts(int IdUser);
        public  Task<ActionResult> AddAlert(int UserDataId, AlertCreacionDTO Alert);
        public  Task<ActionResult> EditAlert(int UserDataId, AlertCreacionDTO Alert);
        public  Task<ActionResult> DeleteAlert(int UserDataId, string coinName);
        public Task DeleteListOfAlerts(List<Alert> listAlert);
    }
}
