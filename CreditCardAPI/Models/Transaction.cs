using System;
using System.ComponentModel.DataAnnotations;

namespace CreditCardAPI.Models
{
    public abstract class Transaction
    {
        public string Type { get; set; }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}