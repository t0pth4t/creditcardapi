using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CreditCardAPI.Models
{
    public class Account
    {
        [Required]
        public int Id { get; set; }

        public Ledger CashOut { get; set; }
        public Ledger Principal { get; set; }
    }
}