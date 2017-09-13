using CreditCardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditCardAPI
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options){ }

        public DbSet<Account> Accounts { get; set; }
    }
}