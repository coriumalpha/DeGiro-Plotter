using AlphaVantage.Net.Stocks;
using AlphaVantage.Net.Stocks.Client;
using SJew.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SJew.AlphaVantage
{
    public class MarketService
    {
        //private string _apiKey = "1";

        //public MarketServiceHandler()
        //{
        //    using var client = new AlphaVantageClient(_apiKey);
        //    using var stocksClient = client.Stocks();
        //}

        public static async Task<SymbolSearchMatch> FindSymbolByISIN(StocksClient stocksClient, string ISIN)
        {
            ICollection<SymbolSearchMatch> searchMatch = await FindSymbol(stocksClient, ISIN);
            return searchMatch.Single();
        }

        public static async Task<ICollection<SymbolSearchMatch>> FindSymbol(StocksClient stocksClient, string keywords)
        {
            ICollection<SymbolSearchMatch> searchMatches = await stocksClient.SearchSymbolAsync(keywords);
            return searchMatches;
        }

        public static async Task<SymbolSearchMatch> FindSymbol(StocksClient stocksClient, Transaction transaction)
        {
            ICollection<SymbolSearchMatch> isinResult = await FindSymbol(stocksClient, transaction.ISIN);
            if (isinResult.Any())
            {
                return isinResult.First();
            }

            ICollection<SymbolSearchMatch> productResult = await FindSymbol(stocksClient, transaction.Product);
            if (productResult.Any())
            {
                return productResult.First();
            }

            string title = String.Format("{0} ({1})", transaction.Product, transaction.ISIN);
            throw new Exception("Can't find symbol for " + title);
        }
    }
}
