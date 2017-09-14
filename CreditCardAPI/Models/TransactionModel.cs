using System.ComponentModel.DataAnnotations;

namespace CreditCardAPI.Models
{
    public class TransactionModel
    {

        [Required]
        public string Type { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public int AccountId { get; set; }
    }
}