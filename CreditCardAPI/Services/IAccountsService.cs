using CreditCardAPI.Models;

namespace CreditCardAPI.Services
{
    public interface IAccountsService
    {
        int CreateAccount();
        AccountModel GetAccount(int accountId);
    }
}