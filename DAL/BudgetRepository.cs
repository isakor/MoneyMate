
using Microsoft.EntityFrameworkCore;
using MoneyMate.Models;

namespace MoneyMate.DAL
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly MoneyMateDbContext _context;

        public BudgetRepository(MoneyMateDbContext context)
        {
            _context = context;
        }

        // Get all budgets
        public async Task<IEnumerable<Budget>> GetAllAsync()
        {
            return await _context.Budgets.ToListAsync();
        }

        // Get budget by ID
        public async Task<Budget?> GetByIdAsync(int id)
        {
            return await _context.Budgets.FindAsync(id);
        }

        // Add a new budget
        public async Task AddAsync(Budget budget)
        {
            await _context.Budgets.AddAsync(budget);
            await _context.SaveChangesAsync();
        }

        // Update an existing budget
        public async Task UpdateAsync(Budget budget)
        {
            _context.Budgets.Update(budget);
            await _context.SaveChangesAsync();
        }

        // Delete a budget
        public async Task DeleteAsync(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);

            if (budget != null)
            {
                _context.Budgets.Remove(budget);
                await _context.SaveChangesAsync();
            }
        }
    }
}
