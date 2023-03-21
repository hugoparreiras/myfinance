using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myfinance_web_netcore.Models
{
    public class RelatorioTransacaoModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<TransacaoModel> Transactions { get; set; }
        public int CountIncomeTransactions { get; set; }
        public int CountExpensesTransactions { get; set; }

        public RelatorioTransacaoModel()
        {
            Transactions = new List<TransacaoModel>();
        }
    }
}