using LoginApplication.API.Data;
using LoginApplication.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UserController(UserDbContext context)
        {
            _context = context;
        }

      
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] users obj)
        {

            var userExists = await _context.User.FirstOrDefaultAsync(u => u.emailId == obj.emailId);
            if (userExists != null)
            {
                return Conflict("Email Id already exists!");
            }

            // Add new user and save to the database
            await _context.User.AddAsync(obj);
            await _context.SaveChangesAsync();

            // Return 201 Created with the URL of the created user resource
            return CreatedAtAction(nameof(GetUserById), new { id = obj.userId }, obj);
        }

        // Login a user
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLogin obj)
        {
            // Find user by email and password
            var user = await _context.User
           .FirstOrDefaultAsync(u => u.emailId == obj.emailId && u.password == obj.password);

            if (user == null)
            {
                return Unauthorized("Invalid Credentials");
            }

            // Return 200 OK with the user information
            return Ok(user);
        }

        // Get all users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userList = await _context.User.ToListAsync();
            if (userList == null || !userList.Any())
            {
                return NotFound("No users found.");
            }

            return Ok(userList);
        }

        // Get user by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }
    }
}
