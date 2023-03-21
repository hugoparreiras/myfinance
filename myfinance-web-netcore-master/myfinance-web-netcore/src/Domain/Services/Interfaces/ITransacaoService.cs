using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Domain.Services.Interfaces
{
    public interface ITransacaoService
    {
        List<TransacaoModel> GetAll();
        TransacaoModel Get(int id);
        void Save(TransacaoModel transactionModel);
        void Delete(int id);
        RelatorioTransacaoModel GetAllByPeriod(DateTime dataInicio, DateTime dataFim);
    }
}