using System;
using System.Collections.Generic;
using System.Linq;
using CreditCardAPI.Models;
using CreditCardAPI.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CreditCardAPI.Tests
{
    public class TransactionsServiceTests
    {
        [Fact]
        public void AddTransaction__GivenAccountId__WhenAccountDoesntExist__ThrowsException()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new DatabaseContext(options))
            {
                var service = new TransactionsService(context);
                Assert.Throws<AccountNotFoundException>(() => service.AddTransaction(0, 0, null));
            }
        }

        [Fact]
        public void AddTransaction__GivenAccountIdAndTransaction__AddsTransactionToCorrectLedgers()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            int id;
            using (var context = new DatabaseContext(options))
            {
                var account = new Account();
                context.Accounts.Add(account);
                context.SaveChanges();
                context.CashOuts.Add(new CashOut { AccountId = account.Id });
                context.Principals.Add(new Principal { AccountId = account.Id });
                context.SaveChanges();
                id = account.Id;
            }

            using (var context = new DatabaseContext(options))
            {
                var service = new TransactionsService(context);
                service.AddTransaction(1, 200, "purchase");

                var principal = context.Principals.Single(x => x.AccountId == id);
                principal.Credits = context.Credits.Where(x => x.LedgerId == principal.Id).ToList();
                principal.Debits = context.Debits.Where(x => x.LedgerId == principal.Id).ToList();
                Assert.Empty(principal.Debits);
                Assert.Single(principal.Credits);
                var pc = principal.Credits.First();
                Assert.Equal(200, pc.Amount);
                Assert.Equal("purchase", pc.Type);

                var cashout = context.CashOuts.Single(x => x.AccountId == id);
                cashout.Credits = context.Credits.Where(x => x.LedgerId == cashout.Id).ToList();
                cashout.Debits = context.Debits.Where(x => x.LedgerId == cashout.Id).ToList();
                Assert.Empty(cashout.Credits);
                Assert.Single(cashout.Debits);
                var cd = cashout.Debits.First();
                Assert.Equal(200, cd.Amount);
                Assert.Equal("purchase", cd.Type);
            }


        }
    }
}