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
        public IActionResult Post([FromBody]Transaction transaction)
        {
            _databaseContext.Transactions.Add(transaction);
            _databaseContext.SaveChanges();
            return Ok();
        }

    }
}
