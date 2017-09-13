using System;
using System.ComponentModel.DataAnnotations;

namespace CreditCardAPI.Models
{
    public class Transaction
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public double Amount { get; set; }

        public DateTime Timestamp { get; set; }
    }
}