using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.CashlessTransactionEntity
{
    [Table("TBL_CASHLESS_TRANSACTIONS_BASIC")]
    internal class CashlessTransactionMini
    {
        [Column("transaction_id")]
        [Key]
        public long TransactionId { get; set; }
        [Column("machine_id")]
        public long MachineId { get; set; }
        [Column("machineAuTime")]
        public DateTime MachineAuTime { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
