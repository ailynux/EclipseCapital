// Controllers/AccountController.cs
using System.Threading.Tasks;
using EclipseCapital.API.Models;
using EclipseCapital.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EclipseCapital.API.Controllers
{
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
            var account = await _accountService.GetAccountAsync(userId);
            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount(string userId)
        {
            var account = await _accountService.CreateAccountAsync(userId);
            return CreatedAtAction(nameof(GetAccount), new { userId = account.UserId }, account);
        }
    }
}
