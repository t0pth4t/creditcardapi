using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CreditCardAPI.Models
{
    public class Account
    {
        [Required]
        public int Id { get; set; }

        public double Principal {
            get
            {
                if (!Transactions.Any())
                    return 0;

                return Transactions.Select(x=>x.Amount).Aggregate((total, transaction) => total + transaction);
            }
        }

        public List<Transaction> Transactions { get; set; }
    }
}