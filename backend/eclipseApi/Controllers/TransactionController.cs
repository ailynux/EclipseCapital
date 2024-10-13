using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using EclipseCapital.API.Models;
using EclipseCapital.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EclipseCapital.API.Controllers
{
    [Authorize]
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
            try
            {
                var transactions = await _transactionService.GetTransactionsAsync(userId);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> CreateTransaction([FromBody] TransactionRequest request)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found");
                }

                var transaction = await _transactionService.CreateTransactionAsync(userId, request.Amount, request.Type);
                return CreatedAtAction(nameof(GetTransactions), new { userId }, transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class TransactionRequest
    {
        public decimal Amount { get; set; }
        public required string Type { get; set; }
    }
}