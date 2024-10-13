using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclipseCapital.API.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public required string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User? User { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}