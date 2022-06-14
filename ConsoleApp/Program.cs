using ConsoleApp.CashlessTransactionEntity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ConsoleApp
{
    class Program
    {
        private const long MachineID = 888888865;

        static void Main()
        {
            QueryDbAsEnumerable();
            QueryDbAsQueryable();

            QueryDbAsRuntimeDetects(isEnumerable: true);
            QueryDbAsRuntimeDetects(isEnumerable: false);
        }

        /// <summary>
        /// Query database with a collection that is determined at runtime.
        /// This will behave the same way as QueryDbAsEnumerable() because the query provider is determined at compilation time.
        /// </summary>
        static void QueryDbAsRuntimeDetects(bool isEnumerable = true)
        {
            Stopwatch watch = Stopwatch.StartNew();

            CashlessTransactionBasicContext context = new CashlessTransactionBasicContext();
            var transactions = context.Transactions;

            var runtimeDeterminedCollection = isEnumerable ? transactions.AsEnumerable() : transactions.AsQueryable();
            var query = from transaction in transactions.AsEnumerable()
                        where transaction.MachineId == MachineID
                        && transaction.MachineAuTime > DateTime.UtcNow.AddDays(-1)
                        orderby transaction.MachineAuTime descending
                        select transaction;

            var result = query.ToList();
            var count = result.Count();
            var fetchedTransaction = result.FirstOrDefault();

            var ellapsedTime = watch.ElapsedMilliseconds;
            Console.WriteLine($"Querying database using IEnumerable fetched {count} transactions took {ellapsedTime} MS.\n" +
                              $"Fetched transaction: {fetchedTransaction}");
        }

        /// <summary>
        /// Querying with IEnumerable will bring all the data to memory first, and only then filters the data
        /// </summary>
        static void QueryDbAsEnumerable()
        {
            Stopwatch watch = Stopwatch.StartNew();

            CashlessTransactionBasicContext context = new CashlessTransactionBasicContext();
            var transactions = context.Transactions;

            var query = from transaction in transactions.AsEnumerable()
                         where transaction.MachineId == MachineID
                         && transaction.MachineAuTime > DateTime.UtcNow.AddDays(-1)
                         orderby transaction.MachineAuTime descending
                         select transaction;

            var result = query.ToList();
            var count = result.Count();
            var fetchedTransaction = result.FirstOrDefault();

            var ellapsedTime = watch.ElapsedMilliseconds;
            Console.WriteLine($"Querying database using IEnumerable fetched {count} transactions took {ellapsedTime} MS.\n" +
                              $"Fetched transaction: {fetchedTransaction}");
        }

        /// <summary>
        /// Querying with IQueryable will filter the data at the 3rd party (database in this case), and then bring the relevant data only.
        /// </summary>
        static void QueryDbAsQueryable()
        {
            Stopwatch watch = Stopwatch.StartNew();

            CashlessTransactionBasicContext context = new CashlessTransactionBasicContext();
            var transactions = context.Transactions;

            var query = from transaction in transactions.AsQueryable()
                         where transaction.MachineId == MachineID
                         && transaction.MachineAuTime > DateTime.UtcNow.AddDays(-1)
                         orderby transaction.MachineAuTime descending
                         select transaction;

            var result = query.ToList();
            var count = result.Count();
            var fetchedTransaction = result.FirstOrDefault();

            var ellapsedTime = watch.ElapsedMilliseconds;
            Console.WriteLine($"Querying database using IQueryable fetched {count} transactions took {ellapsedTime} MS.\n" +
                              $"Fetched transaction: {fetchedTransaction}");
        }
    }
}

