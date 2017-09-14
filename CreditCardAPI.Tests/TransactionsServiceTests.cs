using System;
using System.Collections.Generic;
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
                Assert.Throws<Exception>(() => service.AddTransaction(0, null));
            }
        }

        [Fact]
        public void AddTransaction__GivenAccountIdAndTransaction__AddsTransactionToCorrectLedgers()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new DatabaseContext(options))
            {
                var account = new Account();
                context.Accounts.Add(account);
                context.SaveChanges();
                context.CashOuts.Add(new CashOut { AccountId = account.Id });
                context.Principals.Add(new Principal { AccountId = account.Id });
                context.SaveChanges();
            }

            using (var context = new DatabaseContext(options))
            {
                var service = new TransactionsService(context);
                service.AddTransaction(1, new Credit());
            }
        }
    }
}