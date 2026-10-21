
using Microsoft.EntityFrameworkCore;
using MoneyMate.Models;

namespace MoneyMate.DAL
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MoneyMateDbContext _context;

        public TransactionRepository(MoneyMateDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            // Find the existing transaction in the database
            var existingTransaction =
                await _context.Transactions.FindAsync(transaction.Id);

            if (existingTransaction != null)
            {
                // Update the values of the tracked transaction
                _context.Entry(existingTransaction)
                    .CurrentValues.SetValues(transaction);

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var transaction = await GetByIdAsync(id);

            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }
    }
}
