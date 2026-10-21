
using System.ComponentModel.DataAnnotations;

namespace MoneyMate.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue,
            ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Type is required.")]
        [RegularExpression("^(Income|Expense)$",
            ErrorMessage = "Type must be Income or Expense.")]
        public string Type { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;
    }
}
