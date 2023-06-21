using StockExchange.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace StockExchange.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<UserBalance> UserBalance { get; set; }
        public DbSet<Operations> Operations{ get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<FinancialInstrumnent> FinancialInstrument { get; set; }
        public DbSet<FinancialInstrumnentCategory> FinancialInstrumnentCategories { get; set; }
        public DbSet<UserReport> UserReport { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

            // создаем бд с новой схемой
        }
    }
}
