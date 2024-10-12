// Services/AccountService.cs
using System;
using System.Threading.Tasks;
using EclipseCapital.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EclipseCapital.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetAccountAsync(string userId)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<Account> CreateAccountAsync(string userId)
        {
            var account = new Account
            {
                UserId = userId,
                Balance = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<Account> UpdateBalanceAsync(string userId, decimal amount)
        {
            var account = await GetAccountAsync(userId);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            account.Balance += amount;
            account.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return account;
        }
    }
}