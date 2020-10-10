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

            IEnumerable<IGrouping<string, Transaction>> groups = transactions.GroupBy(x => x.ISIN).OrderBy(x => x.Sum(x => x.Charge.Ammount)).ToList();
            foreach (IGrouping<string, Transaction> group in groups)
            {
                Console.WriteLine(String.Format("{0} [{1}] ({2})", group.FirstOrDefault().Product, group.FirstOrDefault().Price.Currency, group.FirstOrDefault().ISIN));
                Console.WriteLine(String.Format("    Charge: {0} {1}", group.Sum(x => x.Charge.Ammount), group.FirstOrDefault().Charge.Currency));
                Console.WriteLine(String.Format("    Value: {0} {1}", group.Sum(x => x.Value.Ammount), group.FirstOrDefault().Value.Currency));
                Console.WriteLine();
            }

            Console.WriteLine(String.Format("Total charge: {0} EUR", transactions.Sum(x => x.Charge.Ammount)));
            Console.WriteLine(String.Format("Total value: {0} EUR", transactions.Sum(x => x.Value.Ammount)));
            Console.WriteLine(String.Format("Absolute value: {0} EUR", transactions.Sum(x => Math.Abs(x.Value.Ammount.Value))));

            Console.WriteLine("Prog_End");
            Console.ReadKey();
        }
    }
}
