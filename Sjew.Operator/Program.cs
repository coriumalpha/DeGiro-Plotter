using SJew.Business;
using SJew.Entities.Models;
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

            LoaderService loaderService = new LoaderService();
            List<Transaction> transactions = loaderService.ReadTransactions();
            
            AnalyticsService analyticsService = new AnalyticsService(transactions);

            IEnumerable<IGrouping<string, Transaction>> openPositions = analyticsService.GetOpenPositions();

            foreach (IGrouping<string, Transaction> position in openPositions)
            {
                Console.WriteLine(position.First().Product);
                Console.WriteLine(position.Sum(x => x.Quantity));
            }


            Console.WriteLine("Programm_End");
            Console.ReadKey();
        }
    }
}
