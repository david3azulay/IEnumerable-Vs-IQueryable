using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConsoleApp.CashlessTransactionEntity
{
    internal class CashlessTransactionBasicContext : DbContext
    {
        //  TODO: Put connection string here
        const string CONNECTION_STRING = "";
        private readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(config => config.AddConsole());

        public CashlessTransactionBasicContext()
        {
            OnConfiguring(new DbContextOptionsBuilder());
        }

        public DbSet<CashlessTransactionMini> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseLoggerFactory(_loggerFactory);
            dbContextOptionsBuilder.UseSqlServer(connectionString: CONNECTION_STRING);
        }
    }
}
