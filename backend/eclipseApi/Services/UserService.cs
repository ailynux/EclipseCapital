using EclipseCapital.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EclipseCapital.API.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Authenticate a user by checking their username and verifying the password
        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
                return null;

            return user;
        }

        // Verify the hashed password with the stored hash
        private bool VerifyPasswordHash(string password, string storedHash)
        {
            using (var hmac = new HMACSHA512())
            {
                var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return storedHash == computedHash;
            }
        }

        // Register a new user and hash their password before storing it
        public async Task<(bool Success, string Message)> RegisterAsync(string username, string password)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
            {
                return (false, "Username is already taken.");
            }

            byte[] passwordHash = CreatePasswordHash(password);

            var newUser = new User
            {
                Username = username,
                PasswordHash = Convert.ToBase64String(passwordHash),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return (true, "User registered successfully.");
        }

        // Create a hash from the password
        private byte[] CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
