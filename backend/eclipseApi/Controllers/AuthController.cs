using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EclipseCapital.API.Models;

namespace EclipseCapital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // You can replace this with real logic if needed
            if (request.Username == "testuser" && request.Password == "testpassword")
            {
                var token = GenerateJwtToken(new User { Id = Guid.NewGuid().ToString(), Username = request.Username });
                return Ok(new { token, Id = Guid.NewGuid().ToString(), Username = request.Username });
            }
            
            return Unauthorized();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            // Basic implementation without using services for now
            return Ok("User registered successfully.");
        }

        [Authorize]
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            return Ok(new { Id = userId, Username = username });
        }

      private string GenerateJwtToken(User user)
{
    var jwtKey = _configuration["Jwt:Key"];
    if (string.IsNullOrEmpty(jwtKey))
    {
        throw new InvalidOperationException("JWT key not configured");
    }

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.Username)
    };

    var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddHours(3),
        signingCredentials: creds);

    return new JwtSecurityTokenHandler().WriteToken(token);
}

    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Default value for Id
        public string Username { get; set; } = string.Empty; // Default value for Username
    }

}
