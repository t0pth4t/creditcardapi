using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditCardAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardAPI.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public TransactionsController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpPost]
        [Route("api/transactions")]
        public IActionResult Post([FromBody]TransactionModel transaction)
        {
            var account = _databaseContext.Accounts.SingleOrDefault(acc => acc.Id == transaction.AccountId);
            if (account == null)
            {
                return NotFound();
            }
            account.CashOut = _databaseContext.CashOuts.SingleOrDefault(x => x.AccountId == account.Id);
            account.Principal = _databaseContext.Principals.SingleOrDefault(x => x.AccountId == account.Id);
            var debit = new Debit{Amount = transaction.Amount, Timestamp = DateTime.Now, Type = transaction.Type, LedgerId = account.CashOut.Id};
            _databaseContext.Debits.Add(debit);
            
            var credit = new Credit{Amount = transaction.Amount, Timestamp = DateTime.Now, Type = transaction.Type, LedgerId = account.Principal.Id};
            _databaseContext.Credits.Add(credit);

            account.CashOut.Debits.Add(debit);
            account.Principal.Credits.Add(credit);
            
            _databaseContext.SaveChanges();
            return Ok();
        }

    }
}
