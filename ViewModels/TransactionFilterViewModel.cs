
using MoneyMate.Models;

namespace MoneyMate.ViewModels
{
    public class TransactionFilterViewModel
    {
        // List of filtered transactions
        public List<Transaction> Transactions { get; set; }
            = new List<Transaction>();

        // Selected filters
        public int? SelectedMonth { get; set; }

        public int? SelectedYear { get; set; }

        public string? SelectedCategory { get; set; }

        public string? SelectedType { get; set; }

        // Available filter options
        public List<int> Years { get; set; }
            = new List<int>();

        public List<string> Categories { get; set; }
            = new List<string>();

        // Financial summary
        public decimal TotalIncome { get; set; }

        public decimal TotalExpenses { get; set; }

        public decimal Balance { get; set; }
    }
}
