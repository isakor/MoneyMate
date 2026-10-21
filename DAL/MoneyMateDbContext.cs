
using Microsoft.EntityFrameworkCore;
using MoneyMate.Models;

namespace MoneyMate.DAL
{
    public class MoneyMateDbContext : DbContext
    {
        public MoneyMateDbContext(DbContextOptions<MoneyMateDbContext> options)
            : base(options)
        {
        }

        // Transactions table
        public DbSet<Transaction> Transactions { get; set; }

        // Budgets table
        public DbSet<Budget> Budgets { get; set; }
    }
}
