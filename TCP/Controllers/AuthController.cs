using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TCPCommunication.Data;
using TCPCommunication.Models;
using TCPCommunication.DTOs; // 👈 Lägg till denna using!

namespace TCPCommunication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return BadRequest("Email already in use.");
            }

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("Registration successful.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            if (user.Username != loginDto.Username)
            {
                return Unauthorized("Invalid username.");
            }

            return Ok("Login successful.");
        }
    }
}
