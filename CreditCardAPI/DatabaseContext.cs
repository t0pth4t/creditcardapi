using CreditCardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditCardAPI
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options){ }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<CashOut> CashOuts { get; set; }
        public DbSet<Principal> Principals { get; set; }
        public DbSet<Debit> Debits { get; set; }
        public DbSet<Credit> Credits { get; set; }
    }
}