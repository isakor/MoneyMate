
using MoneyMate.Models;

namespace MoneyMate.ViewModels
{
    public class DashboardViewModel
    {
        // Financial summary
        public decimal TotalIncome { get; set; }

        public decimal TotalExpenses { get; set; }

        public decimal Balance { get; set; }

        // Expenses grouped by category
        public Dictionary<string, decimal> ExpensesByCategory { get; set; }
            = new Dictionary<string, decimal>();

        // Monthly budget
        public decimal MonthlyBudget { get; set; }

        public decimal SpentThisMonth { get; set; }

        public decimal RemainingBudget { get; set; }

        public bool HasMonthlyBudget { get; set; }

        // Recent transactions
        public List<Transaction> RecentTransactions { get; set; }
            = new List<Transaction>();

        // Monthly Income vs Expenses chart
        public List<string> MonthLabels { get; set; }
            = new List<string>();

        public List<decimal> MonthlyIncome { get; set; }
            = new List<decimal>();

        public List<decimal> MonthlyExpenses { get; set; }
            = new List<decimal>();

        // Dashboard filters
        public int SelectedMonth { get; set; }

        public int SelectedYear { get; set; }

        public List<int> Years { get; set; }
            = new List<int>();
    }
}
