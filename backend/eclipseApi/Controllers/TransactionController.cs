// Controllers/TransactionController.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using EclipseCapital.API.Models;
using EclipseCapital.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EclipseCapital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(string userId)
        {
            var transactions = await _transactionService.GetTransactionsAsync(userId);
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> CreateTransaction(string userId, [FromBody] TransactionRequest request)
        {
            var transaction = await _transactionService.CreateTransactionAsync(userId, request.Amount, request.Type);
            return CreatedAtAction(nameof(GetTransactions), new { userId }, transaction);
        }
    }

   public class TransactionRequest
{
    public decimal Amount { get; set; }
    public required string Type { get; set; }
}
}