using System.ComponentModel.DataAnnotations;

namespace EclipseCapital.API.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        public required string Username { get; set; } // Add required modifier

        [Required]
        public required string PasswordHash { get; set; } // Add required modifier

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
