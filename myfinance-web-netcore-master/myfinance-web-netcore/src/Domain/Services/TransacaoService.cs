using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myfinance_web_netcore.Domain.Entities;
using myfinance_web_netcore.Domain.Services.Interfaces;
using myfinance_web_netcore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace myfinance_web_netcore.Domain.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly MyFinanceDbContext _dbContext;

        public TransacaoService(MyFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TransacaoModel> GetAll()
        {
            List<TransacaoModel> transactionList = new List<TransacaoModel>();

            try
            {
                using(var dbContext = new MyFinanceDbContext())
                {
                    var dbSet = dbContext.Transacao.Include(x => x.PlanoConta);

                    if(dbSet.Any())
                    {
                        foreach (var item in dbSet)
                        {
                            transactionList.Add(new TransacaoModel
                            {
                                Id = item.Id,
                                DateTransaction = item.Data,
                                History = item.Historico,
                                Value = item.Valor,
                                AccountPlanTransaction =
                                    new PlanoContaModel
                                    {
                                        Id = item.PlanoConta.Id,
                                        Description = item.PlanoConta.Descricao,
                                        Type = item.PlanoConta.Tipo
                                    },
                                AccountPlanId = (int)item.PlanoConta.Id,
                            });
                        }
                    }
                    
                }
                

                return transactionList;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public TransacaoModel Get(int id)
        {
            try
            {
                var dbSet = _dbContext.Transacao
                .Where(x => x.Id.Equals(id))
                .First();

                return new TransacaoModel
                {
                    Id = dbSet.Id,
                    DateTransaction = dbSet.Data,
                    History = dbSet.Historico,
                    Value = dbSet.Valor,
                    AccountPlanId = dbSet.PlanoContaId
                };
            }
            catch (Exception erro)
            {
                throw erro;
            }
            
        }

        public void Save(TransacaoModel transaction)
        {
            var dbSet = _dbContext.Transacao;

            try
            {
                var entity = new Transacao()
                {
                    Id = transaction.Id,
                    Data = transaction.DateTransaction,
                    Historico = transaction.History,
                    Valor = transaction.Value,
                    PlanoContaId = transaction.AccountPlanId
                };

                if (entity.Id == null)
                {
                    dbSet.Add(entity);
                }
                else
                {
                    dbSet.Attach(entity);
                    _dbContext.Entry(entity).State = EntityState.Modified;
                }

                _dbContext.SaveChanges();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Delete(int id)
        {
            var accountPlan = _dbContext.Transacao
                .Where(x => x.Id.Equals(id))
                .First();
            try
            {
                _dbContext.Attach(accountPlan);
                _dbContext.Remove(accountPlan);
                _dbContext.SaveChanges();
            }
            catch(Exception erro)
            {
                throw erro;
            }
            
        }

        public RelatorioTransacaoModel GetAllByPeriod(DateTime startDate, DateTime endDate)
        {

            var dbSet = _dbContext.Transacao
                .Include(x => x.PlanoConta)
                .Where(x => x.Data >= startDate.Date && x.Data <= endDate.Date);

            List<TransacaoModel> transactionList = new List<TransacaoModel>();

            try
            {
                foreach (var item in dbSet)
                {
                    transactionList.Add(new TransacaoModel
                    {
                        Id = item.Id,
                        DateTransaction = item.Data,
                        History = item.Historico,
                        Value = item.Valor,
                        AccountPlanTransaction =
                            new PlanoContaModel
                            {
                                Id = item.PlanoConta.Id,
                                Description = item.PlanoConta.Descricao,
                                Type = item.PlanoConta.Tipo
                            },
                        AccountPlanId = (int)item.PlanoConta.Id,
                        AccountPlanType = item.PlanoConta.Tipo
                    });
                }

                RelatorioTransacaoModel reports = new RelatorioTransacaoModel();
                reports.StartDate = startDate;
                reports.EndDate = endDate;
                reports.Transactions = transactionList;
                reports.CountIncomeTransactions = transactionList
                    .Where(x => x.AccountPlanType.Equals("R"))
                    .ToList()
                    .Count();
                reports.CountExpensesTransactions = transactionList
                    .Where(x => x.AccountPlanType.Equals("D"))
                    .ToList()
                    .Count();
                return reports;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
    }
}