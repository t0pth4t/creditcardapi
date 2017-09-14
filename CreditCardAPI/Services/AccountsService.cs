using System;
using System.Collections.Generic;
using System.Linq;
using CreditCardAPI.Models;

namespace CreditCardAPI.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly DatabaseContext _databaseContext;

        public AccountsService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public int CreateAccount()
        {
            var account = new Account();
            _databaseContext.Accounts.Add(account);
            _databaseContext.SaveChanges();


            _databaseContext.CashOuts.Add(new CashOut { AccountId = account.Id });
            _databaseContext.Principals.Add(new Principal { AccountId = account.Id });
            _databaseContext.SaveChanges();

            return account.Id;
        }

        public AccountModel GetAccount(int accountId)
        {
            var account = _databaseContext.Accounts.SingleOrDefault(x => x.Id == accountId);
            if (account == null)
                throw new AccountNotFoundException($"Account for account id {accountId} not found.");

            PopulateAccountPrincipal(account);
            return new AccountModel
            {
                Id = account.Id,
                Principal = CalculatePrincipal(account.Principal),
                Transactions = GetTransactions(account)
            };
        }

        internal double CalculatePrincipal(Ledger ledger)
        {
            return ledger.Credits.Sum(x => x.Amount);
        }

        internal List<Transaction> GetTransactions(Account account)
        {
            var transactions = new List<Transaction>(account.Principal.Credits);
            transactions.AddRange(account.Principal.Debits);
            return transactions.OrderByDescending(x => x.Timestamp).ToList();
        }

        internal void PopulateAccountPrincipal(Account account)
        {
            account.Principal = _databaseContext.Principals.SingleOrDefault(x => x.AccountId == account.Id);
            account.Principal.Credits = _databaseContext.Credits.Where(x => x.LedgerId == account.Principal.Id).ToList();
            account.Principal.Debits = _databaseContext.Debits.Where(x => x.LedgerId == account.Principal.Id).ToList();
        }
    }
}