using System;
using YahooFinanceApi;

namespace SJew.YahooFinance
{
    public class YahooService
    {
        public async void GetSymbols()
        {
            //await Yahoo.
            // You could query multiple symbols with multiple fields through the following steps:
            var securities = await Yahoo.Symbols("US02079K3059").Fields(Field.Symbol, Field.RegularMarketPrice, Field.FiftyTwoWeekHigh).QueryAsync();

            //var price = first[Field.RegularMarketPrice]; // or, you could use aapl.RegularMarketPrice directly for typed-value
        }
    }
}
