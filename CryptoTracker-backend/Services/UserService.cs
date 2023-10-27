using CryptoTracker_backend.Contexts;
using CryptoTracker_backend.DTOs;
using CryptoTracker_backend.Entities;
using CryptoTracker_backend.Models.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoTracker_backend.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public UserService(ApplicationDbContext context, ITokenService tokenService) {
            _context = context;
            _tokenService = tokenService;
        }
        public (User, UserCredentials) CreateUser(UserCreacionDTO userCreation)
        {

            var user = new User
            {
                LastName = userCreation.LastName,
                FirstName = userCreation.FirstName,
                Email = userCreation.UserEmail
            };

            string bCryptPassword = BCrypt.Net.BCrypt.HashPassword(userCreation.Password);

            var userCredentials = new UserCredentials
            {
                Password = bCryptPassword,
                UserName = userCreation.UserName,
                Role = userCreation.Roles,
                User = user
            };

            return (user, userCredentials);
        }

        public async Task<AuthorizationResponse> SaveNewUser(User user, UserCredentials userCredentials)
        {

            await _context.UsersCredentials.AddAsync(userCredentials);
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return SendResponseWithToken("user Created", userCredentials);
        }

        public async Task<ActionResult> EditUser(int idUser, UserEditDTO userEdit)
        {
            var userData = await _context.Users.FindAsync(idUser);

            if (userData == null)
            {
                return new NotFoundResult();
            }

            userData.Email = userEdit.Email;
            userData.FirstName = userEdit.FirstName;
            userData.LastName = userEdit.LastName;

            _context.Entry(userData).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public AuthorizationResponse SendResponseWithToken(string message, UserCredentials userCredentials)
        {

            AuthorizationResponse auth = new()
            {
                Message = message,
                Token = _tokenService.CreateToken(userCredentials),
                userData = userCredentials.User
            };

            return auth;
        }
    }
}
