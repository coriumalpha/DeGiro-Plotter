using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Stocks;
using AlphaVantage.Net.Stocks.Client;
using SJew.Business;
using SJew.Entities.Models;
using SJew.YahooFinance;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sjew.Operator
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Degiro Plotter");
            Console.WriteLine();

            //ShowWalletTest();
            TestYahoo();

            Console.WriteLine("Programm_End");
            Console.ReadKey();
        }

        static async void TestYahoo()
        {
            YahooService yahooService = new YahooService();
            yahooService.GetSymbols();
        }

        static async void ShowWalletTest()
        {
            LoaderService loaderService = new LoaderService();
            List<Transaction> transactions = loaderService.ReadTransactions();

            AnalyticsService analyticsService = new AnalyticsService(transactions);

            IEnumerable<IGrouping<string, Transaction>> openPositions = analyticsService.GetOpenPositions();


        }
    }
}
