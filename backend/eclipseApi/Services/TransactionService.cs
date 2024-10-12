using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EclipseCapital.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EclipseCapital.API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;

        public TransactionService(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(string userId)
        {
            var account = await _accountService.GetAccountAsync(userId);
            if (account == null)
            {
                return Enumerable.Empty<Transaction>();
            }

            return await _context.Transactions
                .Where(t => t.AccountId == account.Id)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<Transaction> CreateTransactionAsync(string userId, decimal amount, string type)
        {
            var account = await _accountService.GetAccountAsync(userId);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            var transaction = new Transaction
            {
                AccountId = account.Id,
                Amount = amount,
                Type = type,
                CreatedAt = DateTime.UtcNow
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            await _accountService.UpdateBalanceAsync(userId, amount);

            return transaction;
        }
    }
}