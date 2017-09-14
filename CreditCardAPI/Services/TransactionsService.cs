using System;
using System.Linq;
using CreditCardAPI.Models;

namespace CreditCardAPI.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly DatabaseContext _databaseContext;

        public TransactionsService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void AddTransaction(int accountId, double amount, string type)
        {
            var account = _databaseContext.Accounts.SingleOrDefault(acc => acc.Id == accountId);
            if (account == null)
            {
                throw new AccountNotFoundException($"Account for account id {accountId} not found.");
            }

            AddDebit(account.Id, amount, type);
            AddCredit(account.Id, amount, type);
            
        }

        private void AddCredit(int accountId, double amount, string type)
        {
            var principalForAccount = _databaseContext.Principals.SingleOrDefault(x => x.AccountId == accountId);
            var credit = new Credit {Amount = amount, Timestamp = DateTime.Now, Type = type, LedgerId = principalForAccount.Id};
            _databaseContext.Credits.Add(credit);
            _databaseContext.SaveChanges();
        }

        private void AddDebit(int accountId, double amount, string type)
        {
            var cashOutForAccount = _databaseContext.CashOuts.SingleOrDefault(x => x.AccountId == accountId);
            var debit = new Debit {Amount = amount, Timestamp = DateTime.Now, Type = type, LedgerId = cashOutForAccount.Id};
            _databaseContext.Debits.Add(debit);
            _databaseContext.SaveChanges();
        }
    }
}