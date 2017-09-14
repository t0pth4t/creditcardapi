using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditCardAPI.Models
{
    public abstract class Ledger
    {
        public abstract int Id { get; set; }
        public List<Debit> Debits { get; set; }
        public List<Credit> Credits { get; set; }
    }
}