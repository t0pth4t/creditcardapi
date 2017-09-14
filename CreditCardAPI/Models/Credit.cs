using System.ComponentModel.DataAnnotations;

namespace CreditCardAPI.Models
{
    public class Credit : Transaction
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int LedgerId { get; set; }
    }
}