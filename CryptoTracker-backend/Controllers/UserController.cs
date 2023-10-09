using CryptoTracker_backend.Contexts;
using CryptoTracker_backend.DTOs;
using CryptoTracker_backend.Entities;
using CryptoTracker_backend.Models.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoTracker_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(AuthorizationRequest authorization)
        {
            var result = await _context.UsersCredentials.Where(a => a.UserName == authorization.UserName).Include(a => a.User).FirstOrDefaultAsync();

            if (result == null)
            {

                return new BadRequestObjectResult(new {message = "Username y/o Password incorrects" });
            }

            if (BCrypt.Net.BCrypt.Verify(authorization.Password, result.Password))
            {
                AuthorizationResponse response = new AuthorizationResponse
                {
                    Message= "Logeado con exito",
                    userData = result.User,
                    Token = "eue"
                };

                return new JsonResult(response);
            }
            else
            {
                return new BadRequestObjectResult(new { message = "Username y/o Password incorrects" });
            }

           
        }

        [HttpGet("obtain")]
        public async Task<ActionResult<List<User>>> Geta()
        {
            var result = await _context.Users.ToListAsync();

            if (result == null)
            {
                return new NotFoundResult();
            }
            return new JsonResult(result);
        }


        [HttpPost("createUser")]
    public async Task<ActionResult> Post(UserCreacionDTO userCreation) {

            var user = new User
            {
                LastName = userCreation.LastName,
                FirstName = userCreation.FirstName,
                Email = userCreation.UserEmail
            };

            string bCryptPassword = BCrypt.Net.BCrypt.HashPassword(userCreation.Password);
                                    
            var UserCredentials = new UserCredentials
            {
                Password = bCryptPassword,
                UserName = userCreation.UserName,
                User = user
            };

            await _context.UsersCredentials.AddAsync(UserCredentials);
            await _context.Users.AddAsync(user);

            await  _context.SaveChangesAsync();
                return new OkResult();
            


    }

    }
}
