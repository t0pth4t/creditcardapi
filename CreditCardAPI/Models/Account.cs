using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditCardAPI.Models
{
    public class Account
    {
        [Required]
        public int Id { get; set; }

        public double Principal { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}