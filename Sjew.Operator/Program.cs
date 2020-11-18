using SJew.Business;
using SJew.Entities.Models;
using System;
using System.Collections.Generic;

namespace Sjew.Operator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Degiro Plotter");
            Console.WriteLine();

            ShowWalletTest();
            //TestYahoo();

            Console.WriteLine("Programm_End");
            Console.ReadKey();
        }

        static async void ShowWalletTest()
        {
            LoaderService loaderService = new LoaderService();
            List<Transaction> transactions = loaderService.ReadTransactions();
            List<Asset> portfolio = loaderService.ReadPortfolio();

            AnalyticsService analyticsService = new AnalyticsService(transactions, portfolio);

            AmmountCurrency portfolioValue = analyticsService.GetPortfolioTotalValue();
            AmmountCurrency totalValue = analyticsService.GetTotalValue();
            AmmountCurrency totalCharges = analyticsService.GetTotalCharges();

            Console.WriteLine("Total: ", portfolioValue.Ammount + totalValue.Ammount + totalCharges.Ammount);
        }
    }
}
