using CryptoTracker_backend.Contexts;
using CryptoTracker_backend.DTOs;
using CryptoTracker_backend.Entities;
using CryptoTracker_backend.Models.Authorization;
using CryptoTracker_backend.Services;
using CryptoTracker_backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoTracker_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController :  ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public UserController(ApplicationDbContext context , IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpPost("Login")]
        [Consumes("application/json")]
        public async Task<ActionResult> Login([FromBody] AuthorizationRequest authorization)
        {
            var result = await _context.UsersCredentials.Where(a => a.UserName == authorization.UserName).Include(a => a.User).FirstOrDefaultAsync();

            if (result == null)
            {
                return new UnauthorizedObjectResult(new {message = "Username y/o Password incorrects" });
            }

            if (BCrypt.Net.BCrypt.Verify(authorization.Password, result.Password))
            {
                var responseWithToken = _userService.SendResponseWithToken("Logeado con exito", result);

                return new JsonResult(responseWithToken);
            }
            else
            {
                return new UnauthorizedObjectResult(new { message = "Username y/o Password incorrects" });
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
        [Consumes("application/json")]
        public async Task<ActionResult> Post([FromBody]  UserCreacionDTO userCreation) 
        {
            Console.WriteLine(userCreation);
            if (!Roles.IsValidRole(userCreation.Roles))
                return new BadRequestObjectResult(new { message = "The Role is invalid"});

            (User user, UserCredentials userCredentials) =  _userService.CreateUser(userCreation);

            var responseWithToken = await _userService.SaveNewUser(user, userCredentials);

            return new JsonResult(responseWithToken);
        }


        [HttpPut("editUser"),Authorize]
        public async Task<ActionResult> EditUser(int idUser,UserEditDTO userEdit)
        {

            return await _userService.EditUser(idUser, userEdit);

        }

    }
}
