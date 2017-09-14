using System;
using System.Linq;
using CreditCardAPI.Models;

namespace CreditCardAPI.Services
{
    public class TransactionsService
    {
        private readonly DatabaseContext _databaseContext;

        public TransactionsService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void AddTransaction(int accountId, Transaction transaction)
        {
            var account = _databaseContext.Accounts.SingleOrDefault(acc => acc.Id == accountId);
            if (account == null)
            {
                throw new Exception();
            }
            account.CashOut = _databaseContext.CashOuts.SingleOrDefault(x => x.AccountId == account.Id);
            account.Principal = _databaseContext.Principals.SingleOrDefault(x => x.AccountId == account.Id);
            var debit = new Debit { Amount = transaction.Amount, Timestamp = DateTime.Now, Type = transaction.Type, LedgerId = account.CashOut.Id };
            _databaseContext.Debits.Add(debit);

            var credit = new Credit { Amount = transaction.Amount, Timestamp = DateTime.Now, Type = transaction.Type, LedgerId = account.Principal.Id };
            _databaseContext.Credits.Add(credit);

            account.CashOut.Debits.Add(debit);
            account.Principal.Credits.Add(credit);

            _databaseContext.SaveChanges();
        }
    }
}