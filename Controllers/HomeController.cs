
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MoneyMate.DAL;
using MoneyMate.Models;
using MoneyMate.ViewModels;

namespace MoneyMate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBudgetRepository _budgetRepository;

        public HomeController(
            ITransactionRepository transactionRepository,
            IBudgetRepository budgetRepository)
        {
            _transactionRepository = transactionRepository;
            _budgetRepository = budgetRepository;
        }

        // GET: Home/Index
        public async Task<IActionResult> Index(
            int? selectedMonth,
            int? selectedYear)
        {
            // Get all transactions
            var allTransactions =
                await _transactionRepository.GetAllAsync();

            // Get all budgets
            var budgets =
                await _budgetRepository.GetAllAsync();

            // Use current month and year as default
            var month = selectedMonth ?? DateTime.Today.Month;
            var year = selectedYear ?? DateTime.Today.Year;

            // Validate selected month
            if (month < 1 || month > 12)
            {
                month = DateTime.Today.Month;
            }

            // Create list of available years
            var years = allTransactions
                .Select(t => t.Date.Year)
                .Concat(budgets.Select(b => b.Year))
                .Append(DateTime.Today.Year)
                .Distinct()
                .OrderByDescending(y => y)
                .ToList();

            // Include selected year
            if (!years.Contains(year))
            {
                years.Add(year);
                years = years.OrderByDescending(y => y).ToList();
            }

            // Filter transactions by month and year
            var transactions = allTransactions
                .Where(t =>
                    t.Date.Month == month &&
                    t.Date.Year == year)
                .ToList();

            // Calculate total income
            var totalIncome = transactions
                .Where(t => t.Type == "Income")
                .Sum(t => t.Amount);

            // Calculate total expenses
            var totalExpenses = transactions
                .Where(t => t.Type == "Expense")
                .Sum(t => t.Amount);

            // Calculate balance
            var balance = totalIncome - totalExpenses;

            // Calculate expenses by category
            var expensesByCategory = transactions
                .Where(t => t.Type == "Expense")
                .GroupBy(t => t.Category)
                .ToDictionary(
                    group => group.Key,
                    group => group.Sum(t => t.Amount)
                );

            // Find budget for selected month and year
            var currentBudget = budgets
                .FirstOrDefault(b =>
                    b.Month == month &&
                    b.Year == year);

            // Calculate spending for selected month
            var spentThisMonth = totalExpenses;

            // Get monthly budget
            var monthlyBudget =
                currentBudget?.Amount ?? 0;

            // Calculate remaining budget
            var remainingBudget =
                monthlyBudget - spentThisMonth;

            // Get five most recent transactions
            // within the selected period
            var recentTransactions = transactions
                .OrderByDescending(t => t.Date)
                .ThenByDescending(t => t.Id)
                .Take(5)
                .ToList();

            // Monthly Income vs Expenses chart
            // Show all 12 months for selected year

            var monthLabels = new List<string>();
            var monthlyIncome = new List<decimal>();
            var monthlyExpenses = new List<decimal>();

            for (int currentMonth = 1; currentMonth <= 12; currentMonth++)
            {
                var monthName =
                    System.Globalization.CultureInfo
                        .GetCultureInfo("en-US")
                        .DateTimeFormat
                        .GetAbbreviatedMonthName(currentMonth);

                monthLabels.Add(monthName);

                // Income for month in selected year
                var income = allTransactions
                    .Where(t =>
                        t.Type == "Income" &&
                        t.Date.Month == currentMonth &&
                        t.Date.Year == year)
                    .Sum(t => t.Amount);

                // Expenses for month in selected year
                var expenses = allTransactions
                    .Where(t =>
                        t.Type == "Expense" &&
                        t.Date.Month == currentMonth &&
                        t.Date.Year == year)
                    .Sum(t => t.Amount);

                monthlyIncome.Add(income);
                monthlyExpenses.Add(expenses);
            }

            // Create DashboardViewModel
            var dashboard = new DashboardViewModel
            {
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                Balance = balance,

                ExpensesByCategory = expensesByCategory,

                MonthlyBudget = monthlyBudget,
                SpentThisMonth = spentThisMonth,
                RemainingBudget = remainingBudget,
                HasMonthlyBudget = currentBudget != null,

                RecentTransactions = recentTransactions,

                MonthLabels = monthLabels,
                MonthlyIncome = monthlyIncome,
                MonthlyExpenses = monthlyExpenses,

                SelectedMonth = month,
                SelectedYear = year,
                Years = years
            };

            return View(dashboard);
        }

        // GET: Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Home/Error
        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}
