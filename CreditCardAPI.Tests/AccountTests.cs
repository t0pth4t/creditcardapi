//using System.Collections.Generic;
//using CreditCardAPI.Models;
//using Xunit;

//namespace CreditCardAPI.Tests
//{
//    public class AccountTests
//    {
//        [Fact]
//        public void GetPrincipal__WhenNoTransactions__ReturnsZero()
//        {
//            var account = new Account
//            {
//                Transactions = new List<Transaction>()
//            };

//            var result = account.Principal;

//            Assert.Equal(0, result);
//        }

//        [Fact]
//        public void GetPrincipal__WhenTransactions__ReturnsPrincipal()
//        {
//            var account = new Account
//            {
//                Transactions = new List<Transaction>
//                {
//                    new Transaction
//                    {
//                        Amount = 1.1
//                    },
//                    new Transaction
//                    {
//                        Amount = -0.1
//                    }
//                }
//            };

//            var result = account.Principal;

//            Assert.Equal(1, result);
//        }
//    }
//}