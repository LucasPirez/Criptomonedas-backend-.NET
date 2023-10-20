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
        private readonly ITokenService _tokenService;
      
        public UserController(ApplicationDbContext context , ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(AuthorizationRequest authorization)
        {
            var result = await _context.UsersCredentials.Where(a => a.UserName == authorization.UserName).Include(a => a.User).FirstOrDefaultAsync();

            if (result == null)
            {
                return new UnauthorizedObjectResult(new {message = "Username y/o Password incorrects" });
            }

            if (BCrypt.Net.BCrypt.Verify(authorization.Password, result.Password))
            {
                AuthorizationResponse response = new()
                {
                    Message= "Logeado con exito",
                    userData = result.User,
                    Token = _tokenService.CreateToken(result)
                };

                return new JsonResult(response);
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
        public async Task<ActionResult> Post(UserCreacionDTO userCreation) {


            if (!Roles.IsValidRole(userCreation.Roles))
                return new BadRequestObjectResult(new { message = "The Role is invalid"});

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
                Role = userCreation.Roles,
                User = user
            };

            await _context.UsersCredentials.AddAsync(UserCredentials);
            await _context.Users.AddAsync(user);

            await  _context.SaveChangesAsync();

            AuthorizationResponse response = new()
            {

                Message = "Usuer Created",
                Token = _tokenService.CreateToken(UserCredentials),
                userData = user

            };

            return new JsonResult(response);
           
        }

        [HttpPut("editUser"),Authorize]
        public async Task<ActionResult> EditUser(int idUser,UserEditDTO userEdit)
        {
            var userData = await _context.Users.FindAsync(idUser);

            if (userData == null)
            {
                return new NotFoundResult(); // Otra opción podría ser retornar un BadRequest() dependiendo de la lógica de tu aplicación
            }

            userData.Email = userEdit.Email;
            userData.FirstName = userEdit.FirstName;
            userData.LastName = userEdit.LastName;

            _context.Entry(userData).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            
            return new OkResult();

        }

        

    }
}
