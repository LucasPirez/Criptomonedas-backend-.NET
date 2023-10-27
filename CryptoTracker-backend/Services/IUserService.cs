using CryptoTracker_backend.DTOs;
using CryptoTracker_backend.Entities;
using CryptoTracker_backend.Models.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTracker_backend.Services
{
    public interface IUserService
    {
        (User, UserCredentials) CreateUser(UserCreacionDTO userCreation);
         Task<AuthorizationResponse> SaveNewUser(User user, UserCredentials userCredentials);

        Task<ActionResult> EditUser(int idUser, UserEditDTO userEdit);

        AuthorizationResponse SendResponseWithToken(string message, UserCredentials userCredentials);
    }
}
