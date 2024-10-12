// Models/Transaction.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclipseCapital.API.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        
        public required string Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}