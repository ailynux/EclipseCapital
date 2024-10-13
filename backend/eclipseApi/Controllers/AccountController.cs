using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EclipseCapital.API.Services;
using EclipseCapital.API.Models;

namespace EclipseCapital.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<Account>> GetAccount(string userId)
        {
            try
            {
                var account = await _accountService.GetAccountAsync(userId);
                if (account == null)
                {
                    return NotFound("Account not found");
                }

                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found");
                }

                var account = await _accountService.CreateAccountAsync(userId);
                return CreatedAtAction(nameof(GetAccount), new { userId = account.UserId }, account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}