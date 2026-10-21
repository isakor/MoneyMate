
using MoneyMate.Models;

namespace MoneyMate.DAL
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();

        Task<Transaction?> GetByIdAsync(int id);

        Task AddAsync(Transaction transaction);

        Task UpdateAsync(Transaction transaction);

        Task DeleteAsync(int id);
    }
}
