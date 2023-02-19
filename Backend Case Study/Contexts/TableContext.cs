using Backend_Case_Study.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_Case_Study.Contexts
{
    public class TableContext : DbContext
    {
        public TableContext(DbContextOptions<TableContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

    }
}
