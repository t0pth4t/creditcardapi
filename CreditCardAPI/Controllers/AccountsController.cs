using System;
using CreditCardAPI.Models;
using CreditCardAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardAPI.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountsService _accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpGet]
        [Route("api/health")]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/accounts/{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            try
            {
                var account = _accountsService.GetAccount(id);
                return Ok(account);
            }
            catch (AccountNotFoundException ae)
            {
                return NotFound(ae);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }

        [HttpPost]
        [Route("api/accounts")]
        public IActionResult Post([FromBody] AccountModel newAccount)
        {
            try
            {
                var accountId = _accountsService.CreateAccount();
                return Ok(accountId);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

    }
}
