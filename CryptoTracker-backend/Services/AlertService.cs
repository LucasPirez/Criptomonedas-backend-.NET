using CryptoTracker_backend.Contexts;
using CryptoTracker_backend.DTOs;
using CryptoTracker_backend.entities;
using CryptoTracker_backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoTracker_backend.Services
{
    public class AlertService : IAlertService
    {
        private readonly ApplicationDbContext _context;

        public AlertService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetCoinsInAlerts()
        {
           List<CoinInAlert> coinsResponse = await _context.CoinsInAlerts.Where(u=> u.AlertWithThisCoin.Any()).ToListAsync();

            string CoinString = "";

            foreach (var value in coinsResponse)
            {
                CoinString += value.CoinId + ",";
            }

            return CoinString[..^1]; 
            
        }
     
        public async Task<List<User>> GetUsersWithAlerts()
        { 
            List<User> response = await _context.Users
                .Where(u => u.Alerts.Any()).Include(a => a.Alerts).ToListAsync();

            return response;
        }

        public async Task<List<Alert>> GetAllAlerts()
        {
            var response =  await _context.Alerts.ToListAsync();
            return response;
        }
        
        public async Task<List<AlertResponseDTO>> GetUserAlerts(int IdUser)
        {
         return   await _context.Alerts.Where(a => a.UserId == IdUser).Select(a => new AlertResponseDTO
            {
              CoinId =  a.Coin.CoinId,
              DateCreate =  a.DateCreate,
               MinPrice = a.MinPrice,
              MaxPrice =  a.MaxPrice
            }).ToListAsync();
        }

        public async Task<ActionResult> AddAlert(int UserDataId, AlertCreacionDTO Alert)
        {
            var isCoinUsed = await _context.CoinsInAlerts.Where(a => a.CoinId == Alert.CoinName).FirstOrDefaultAsync();

            CoinInAlert Coin;
            if (isCoinUsed == null)
            {
                Coin = new CoinInAlert()
                {
                    CoinId = Alert.CoinName
                };
            }else
            {
                Coin = isCoinUsed;
            }

            var alert = new Alert()
            {
                Coin = Coin,
                MaxPrice = Alert.MaxPrice,
                MinPrice = Alert.MinPrice,
                DateCreate = Alert.DateCreate,
                UserId = UserDataId

            };

            _context.Alerts.Add(alert);

            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<ActionResult> EditAlert(int UserDataId, AlertCreacionDTO Alert)
        {

            var alertToEdit = await _context.Alerts.Where(
                a => a.UserId == UserDataId && a.Coin.CoinId == Alert.CoinName)
                 .FirstOrDefaultAsync();

            if (alertToEdit == null)
            {
                return new NotFoundObjectResult(new { message = "No alert was found for that cryptocurrency, please try again " });
            }

            alertToEdit.MinPrice = Alert.MinPrice;
            alertToEdit.MaxPrice = Alert.MaxPrice;
            alertToEdit.DateCreate = new DateTime();

            _context.Entry(alertToEdit).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<ActionResult> DeleteAlert(int UserDataId, string CoinName)
        {

            var alertToEdit = await _context.Alerts.Where(
                a => a.UserId == UserDataId && a.Coin.CoinId == CoinName)
                 .FirstOrDefaultAsync();

            if (alertToEdit == null)
            {
                return new NotFoundObjectResult(new { message = "No alert was found for that cryptocurrency, please try again " });
            }

            _context.Alerts.Remove(alertToEdit);

            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}



