using logintest.Data;
using logintest.Models;
using Microsoft.AspNetCore.Mvc;

namespace logintest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User userDto)
        {
            if (_context.Users.Any(u => u.Email == userDto.Email))
                return BadRequest("Email already exists");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var user = new User
            {
                Email = userDto.Email,
                Password = hashedPassword
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User userDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userDto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password);

            if (!isPasswordValid)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok("Login successful");
        }
    }
}