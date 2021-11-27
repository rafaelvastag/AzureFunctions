using Microsoft.EntityFrameworkCore;

namespace CodeFirstUnitOfWork.Models
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options)
        : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }

    }
}

