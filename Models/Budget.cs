
using System.ComponentModel.DataAnnotations;

namespace MoneyMate.Models
{
    public class Budget
    {
        public int Id { get; set; }

        [Range(0.01, double.MaxValue,
            ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Range(1, 12,
            ErrorMessage = "Month must be between 1 and 12.")]
        public int Month { get; set; }

        [Range(2000, 2100,
            ErrorMessage = "Year must be between 2000 and 2100.")]
        public int Year { get; set; }
    }
}
