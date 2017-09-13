using System.ComponentModel.DataAnnotations;

namespace CreditCardAPI.Models
{
    public class Account
    {
        [Required]
        public int Id { get; set; }
    }
}