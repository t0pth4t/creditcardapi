using System.Collections.Generic;
using System.Linq;

namespace CreditCardAPI.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public double Principal { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}