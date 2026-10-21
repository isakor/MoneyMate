
using Microsoft.AspNetCore.Mvc;
using MoneyMate.DAL;
using MoneyMate.Models;

namespace MoneyMate.Controllers
{
    public class BudgetController : Controller
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetController(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        // Show all budgets
        public async Task<IActionResult> Index()
        {
            var budgets = await _budgetRepository.GetAllAsync();

            return View(budgets);
        }

        // Show form for creating a budget
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Save a new budget
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Budget budget)
        {
            // Get existing budgets
            var budgets = await _budgetRepository.GetAllAsync();

            // Check if a budget already exists for this month and year
            var budgetExists = budgets.Any(b =>
                b.Month == budget.Month &&
                b.Year == budget.Year);

            if (budgetExists)
            {
                ModelState.AddModelError(
                    "",
                    "A budget already exists for this month and year."
                );
            }

            if (!ModelState.IsValid)
            {
                return View(budget);
            }

            await _budgetRepository.AddAsync(budget);

            return RedirectToAction(nameof(Index));
        }

        // Show form for editing a budget
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var budget = await _budgetRepository.GetByIdAsync(id);

            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // Save changes to a budget
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Budget budget)
        {
            if (id != budget.Id)
            {
                return BadRequest();
            }

            // Check if another budget exists for this month and year
            var budgets = await _budgetRepository.GetAllAsync();

            var budgetExists = budgets.Any(b =>
                b.Month == budget.Month &&
                b.Year == budget.Year &&
                b.Id != budget.Id);

            if (budgetExists)
            {
                ModelState.AddModelError(
                    "",
                    "A budget already exists for this month and year."
                );
            }

            if (!ModelState.IsValid)
            {
                return View(budget);
            }

            var existingBudget =
                await _budgetRepository.GetByIdAsync(id);

            if (existingBudget == null)
            {
                return NotFound();
            }

            existingBudget.Amount = budget.Amount;
            existingBudget.Month = budget.Month;
            existingBudget.Year = budget.Year;

            await _budgetRepository.UpdateAsync(existingBudget);

            return RedirectToAction(nameof(Index));
        }

        // Delete a budget
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var budget = await _budgetRepository.GetByIdAsync(id);

            if (budget == null)
            {
                return NotFound();
            }

            await _budgetRepository.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
