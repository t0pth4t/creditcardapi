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
            account.CashOut = _databaseContext.CashOuts.SingleOrDefault(x => x.AccountId == account.Id);
            account.CashOut.Credits = _databaseContext.Credits.Where(x => x.LedgerId == account.CashOut.Id).ToList();
            account.CashOut.Debits = _databaseContext.Debits.Where(x => x.LedgerId == account.CashOut.Id).ToList();
            List<Transaction> transactions = new List<Transaction>(account.CashOut.Credits);
            transactions.AddRange(account.CashOut.Debits);
            var principal = account.CashOut.Credits.Sum(x => x.Amount) - account.CashOut.Debits.Sum(x => x.Amount);
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
