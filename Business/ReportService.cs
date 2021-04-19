using SJew.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SJew.Business
{
    public class ReportService
    {
        private List<Transaction> _transactions;
        private List<Asset> _portfolio;

        public ReportService(List<Transaction> transactions, List<Asset> portfolio)
        {
            _transactions = transactions;
            _portfolio = portfolio;
        }

        public List<DayTransactions> TransactionsPerDay()
        {
            Dictionary<DateTime, List<Transaction>> transactionsPerDay = _transactions
                .OrderBy(x => x.Date)
                .GroupBy(x => x.Date.Date)
                .ToDictionary(x => x.Key, x => x.ToList());

            List<DayTransactions> dayTransactions = transactionsPerDay
                .Select(x => new DayTransactions()
                {
                    Date = x.Key,
                    Value = new AmmountCurrency(x.Value.Sum(x => x.Value.Ammount), x.Value.First().Value.Currency),
                    Charge = new AmmountCurrency(x.Value.Sum(x => x.Charge.Ammount), x.Value.First().Charge.Currency),
                    Total = new AmmountCurrency(x.Value.Sum(x => x.Total.Ammount), x.Value.First().Total.Currency)
                }).ToList();

            return dayTransactions;
        }
    }
}
