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

        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        [Route("api/health")]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/values/5
        [HttpGet]
        [Route("api/accounts/{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            var account =  _databaseContext.Accounts.SingleOrDefault(x => x.Id == id);
            if (account == null)
                return NotFound();
            return Ok(account);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Account account)
        {
            _databaseContext.Accounts.Add(account);
            _databaseContext.SaveChanges();
            return Ok();
        }

    }
}
