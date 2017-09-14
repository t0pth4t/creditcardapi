using System.ComponentModel.DataAnnotations;

namespace CreditCardAPI.Models
{
    public class CashOut : Ledger
    {
        [Required]
        public override int Id { get; set; }
        [Required]
        public int AccountId { get; set; }
    }
}