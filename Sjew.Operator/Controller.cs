using SJew.Business;
using SJew.Entities.Models;
using System;
using System.Collections.Generic;

namespace Sjew.Operator
{
    public class Controller
    {
        private AnalyticsService _analyticsService;

        public Controller()
        {
            _analyticsService = GetAnalyticsService();
        }

        private AnalyticsService GetAnalyticsService()
        {
            LoaderService loaderService = new LoaderService();
            List<Transaction> transactions = loaderService.ReadTransactions();
            List<Asset> portfolio = loaderService.ReadPortfolio();

            return new AnalyticsService(transactions, portfolio);
        }

        public void ShowTotalRevenue(bool fromLocalValue = true)
        {
            AmmountCurrency portfolioValue = _analyticsService.GetPortfolioTotalValue();
            AmmountCurrency totalValue = _analyticsService.GetTotalValue(fromLocalValue);
            AmmountCurrency totalCharges = _analyticsService.GetTotalCharges();

            double totalRevenue = (portfolioValue.Ammount + totalValue.Ammount + totalCharges.Ammount) ?? 0;

            Console.WriteLine("Total revenue: " + totalRevenue.ToString("n2") + " EUR");
        }
    }
}
