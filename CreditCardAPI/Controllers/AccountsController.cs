using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditCardAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardAPI.Controllers
{
    public class AccountsController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public AccountsController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
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
            var account =  _databaseContext.Accounts.SingleOrDefault(x => x.Id == id);
            if (account == null)
                return NotFound();
            account.Principal = _databaseContext.Principals.SingleOrDefault(x => x.AccountId == account.Id);
            account.Principal.Credits = _databaseContext.Credits.Where(x => x.LedgerId == account.Principal.Id).ToList();
            account.Principal.Debits = _databaseContext.Debits.Where(x => x.LedgerId == account.Principal.Id).ToList();
            List<Transaction> transactions = new List<Transaction>(account.Principal.Credits);
            transactions.AddRange(account.Principal.Debits);
            var principal = account.Principal.Credits.Sum(x => x.Amount);
            var accountModel = new AccountModel
            {
                Id = account.Id,
                Principal = principal,
                Transactions    = transactions.OrderByDescending(x => x.Timestamp)
            };
            return Ok(accountModel);
        }

        [HttpPost]
        [Route("api/accounts")]
        public IActionResult Post([FromBody]AccountModel newAccount)
        {

            var account = new Account();
            _databaseContext.Accounts.Add(account);
            _databaseContext.SaveChanges();


            _databaseContext.CashOuts.Add(new CashOut {AccountId = account.Id});
            _databaseContext.Principals.Add(new Principal { AccountId = account.Id});
            _databaseContext.SaveChanges();
            return Ok(account.Id);
        }

    }
}
