using CryptoTracker_backend.Contexts;
using CryptoTracker_backend.Entities;
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


        [HttpGet("obtain")]
        public async Task<ActionResult<List<User>>> Get()
        {
            var result = await _context.Users.ToListAsync();
            if (result == null)
            {
                return new NotFoundResult();
            }
            return new JsonResult(result);
        }

    [HttpPost("createUser")]
    public async Task<ActionResult> Post(User user)
    {
       await _context.Users.AddAsync(user);

       await  _context.SaveChangesAsync();
                return new OkResult();
            


    }

    }
}
