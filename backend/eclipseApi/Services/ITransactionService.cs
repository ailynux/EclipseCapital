// Services/ITransactionService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using EclipseCapital.API.Models;

namespace EclipseCapital.API.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync(string userId);
        Task<Transaction> CreateTransactionAsync(string userId, decimal amount, string type);
    }
}