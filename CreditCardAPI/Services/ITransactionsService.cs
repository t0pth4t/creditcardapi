using CreditCardAPI.Models;

namespace CreditCardAPI.Services
{
    public interface ITransactionsService
    {
        void AddTransaction(int accountId, double amount, string type);
    }
}