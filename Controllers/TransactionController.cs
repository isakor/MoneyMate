
using Microsoft.AspNetCore.Mvc;
using MoneyMate.DAL;
using MoneyMate.Models;
using MoneyMate.ViewModels;

namespace MoneyMate.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(
            ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // GET: Transaction
        // Show and filter transactions
        public async Task<IActionResult> Index(
            int? selectedMonth,
            int? selectedYear,
            string? selectedCategory,
            string? selectedType)
        {
            // Get all transactions
            var transactions =
                await _transactionRepository.GetAllAsync();

            // Get available years before filtering
            var years = transactions
                .Select(t => t.Date.Year)
                .Distinct()
                .OrderByDescending(y => y)
                .ToList();

            // Get available categories before filtering
            var categories = transactions
                .Select(t => t.Category)
                .Where(c => !string.IsNullOrEmpty(c))
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            // Filter by month
            if (selectedMonth.HasValue)
            {
                transactions = transactions
                    .Where(t => t.Date.Month == selectedMonth.Value)
                    .ToList();
            }

            // Filter by year
            if (selectedYear.HasValue)
            {
                transactions = transactions
                    .Where(t => t.Date.Year == selectedYear.Value)
                    .ToList();
            }

            // Filter by category
            if (!string.IsNullOrEmpty(selectedCategory))
            {
                transactions = transactions
                    .Where(t => t.Category == selectedCategory)
                    .ToList();
            }

            // Filter by type
            if (!string.IsNullOrEmpty(selectedType))
            {
                transactions = transactions
                    .Where(t => t.Type == selectedType)
                    .ToList();
            }

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

            // Sort transactions by newest date
            var sortedTransactions = transactions
                .OrderByDescending(t => t.Date)
                .ThenByDescending(t => t.Id)
                .ToList();

            // Create ViewModel
            var viewModel = new TransactionFilterViewModel
            {
                Transactions = sortedTransactions,

                SelectedMonth = selectedMonth,
                SelectedYear = selectedYear,
                SelectedCategory = selectedCategory,
                SelectedType = selectedType,

                Years = years,
                Categories = categories,

                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                Balance = balance
            };

            return View(viewModel);
        }

        // GET: Transaction/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return View(transaction);
            }

            await _transactionRepository.AddAsync(transaction);

            return RedirectToAction(nameof(Index));
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var transaction =
                await _transactionRepository.GetByIdAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(transaction);
            }

            var existingTransaction =
                await _transactionRepository.GetByIdAsync(id);

            if (existingTransaction == null)
            {
                return NotFound();
            }

            await _transactionRepository.UpdateAsync(transaction);

            return RedirectToAction(nameof(Index));
        }

        // POST: Transaction/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction =
                await _transactionRepository.GetByIdAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            await _transactionRepository.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
