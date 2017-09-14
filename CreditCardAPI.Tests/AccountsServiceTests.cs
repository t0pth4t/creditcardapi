using System;
using System.Collections.Generic;
using System.Linq;
using CreditCardAPI.Models;
using CreditCardAPI.Services;
using Xunit;

namespace CreditCardAPI.Tests
{
    public class AccountsServiceTests
    {
        [Fact]
        public void CalculatePrincipal__GivenLedger__CalculatesPrincipal()
        {
            var ledger = new Principal
            {
                Credits = new List<Credit>
                {
                    new Credit {Amount = 200},
                    new Credit {Amount = 500},
                    new Credit {Amount = 750}
                }
            };
            
            var accountsService = new AccountsService(null);

            var result = accountsService.CalculatePrincipal(ledger);
            Assert.Equal(1450, result);
        }

        [Fact]
        public void GetTransactions__GivenAccount__ReturnsAllTransactionListInDescendingOrder()
        {
            var account = new Account
            {
                Principal = new Principal
                {
                    Credits = new List<Credit>
                    {
                        new Credit {Timestamp = DateTime.Now.AddDays(10),Type = "first"},
                    },
                    Debits = new List<Debit>
                    {
                        new Debit{Timestamp = DateTime.Now.AddDays(-10), Type = "second"},
                        new Debit{Timestamp = DateTime.Now.AddYears(-10), Type = "third"}
                    }
                }
            };

            var accounts = new AccountsService(null);

            var results = accounts.GetTransactions(account);

            Assert.Equal(3, results.Count);
            Assert.Equal("first", results[0].Type);
            Assert.Equal("second", results[1].Type);
            Assert.Equal("third", results[2].Type);
        }
    }
}
