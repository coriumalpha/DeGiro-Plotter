using CsvHelper.Configuration;
using SJew.Entities.Models;
using System;

namespace SJew.Business
{
    public class TransactionMapper : ClassMap<Transaction>
    {
        public TransactionMapper()
        {
            Map(m => m.Id).Index(18);
            Map(m => m.Date).ConvertUsing(row => DateTime.Parse(String.Join(" ", row.GetField<String>(0), row.GetField<String>(1))));
            Map(m => m.Product).Index(2);
            Map(m => m.ISIN).Index(3);
            Map(m => m.ExchangeMarket).Index(4);
            Map(m => m.ExecutionCenter).Index(5);
            Map(m => m.Quantity).Index(6);
            Map(m => m.ForeignExchangeRate).Index(13);
            Map(m => m.Price).ConvertUsing(row => new AmmountCurrency(row.GetField<double>(7), row.GetField<String>(8)));
            Map(m => m.LocalValue).ConvertUsing(row => new AmmountCurrency(row.GetField<double>(9), row.GetField<String>(10)));
            Map(m => m.Value).ConvertUsing(row => new AmmountCurrency(row.GetField<double>(11), row.GetField<String>(12)));
            Map(m => m.Charge).ConvertUsing(row => new AmmountCurrency(row.GetField<double?>(14), row.GetField<String>(15)));
            Map(m => m.Total).ConvertUsing(row => new AmmountCurrency(row.GetField<double>(16), row.GetField<String>(17)));
        }
    }
}