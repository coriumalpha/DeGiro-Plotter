using CsvHelper.Configuration;
using SJew.Entities.Models.Base;
using System;

namespace SJew.Business
{
    public class AssetMapper : ClassMap<Asset>
    {
        public AssetMapper()
        {
            Map(m => m.Id).ConvertUsing(row => new Guid());
            Map(m => m.Product).Index(0);
            Map(m => m.SymbolOrISIN).Index(1);
            Map(m => m.Quantity).Index(2);
            Map(m => m.LastPrice).ConvertUsing(row => string.IsNullOrEmpty(row.GetField<string>(3)) ? null : new AmmountCurrency(row.GetField<double>(3), row.GetField<String>(4).Substring(0, 3)));
            Map(m => m.LocalValue).ConvertUsing(row => ParseLocalValue(row.GetField<string>(4).Replace('.', ',')));
            Map(m => m.EURValue).ConvertUsing(row => new AmmountCurrency(row.GetField<double>(5), "EUR"));
        }

        private AmmountCurrency ParseLocalValue(string rawLocalValue)
        {
            string[] localValueParts = rawLocalValue.Split();

            AmmountCurrency localValue = new AmmountCurrency()
            {
                Currency = localValueParts[0],
                Ammount = double.Parse(localValueParts[1])
            };

            return localValue;
        }
    }
}