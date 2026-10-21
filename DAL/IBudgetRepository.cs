
using MoneyMate.Models;

namespace MoneyMate.DAL
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<Budget>> GetAllAsync();

        Task<Budget?> GetByIdAsync(int id);

        Task AddAsync(Budget budget);

        Task UpdateAsync(Budget budget);

        Task DeleteAsync(int id);
    }
}
