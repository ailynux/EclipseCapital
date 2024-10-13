using EclipseCapital.API.Models;

namespace EclipseCapital.API.Services
{
    public interface IAccountService
    {
        Task<Account?> GetAccountAsync(string userId);
        Task<Account> CreateAccountAsync(string userId);
        Task<Account> UpdateBalanceAsync(string userId, decimal amount);
    }
}