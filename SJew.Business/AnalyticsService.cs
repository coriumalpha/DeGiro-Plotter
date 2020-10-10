using CsvHelper;
using SJew.Entities.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SJew.Business
{
    public class AnalyticsService
    {
        private List<Transaction> _transactions;

        public AnalyticsService(List<Transaction> transactions)
        {
            _transactions = transactions;
        }

        /// <summary>
        /// Calcula el valor absoluto de capital movido en concepto de valor de las operaciones
        /// </summary>
        /// <returns></returns>
        public AmmountCurrency GetAbsoluteOperatedVolume()
        {
            //TODO: Asume que todas las transacciones de Value tienen la misma divisa
            AmmountCurrency absoluteOperatedVolume = new AmmountCurrency()
            {
                Ammount = _transactions.Sum(x => Math.Abs(x.Value.Ammount.Value)),
                Currency = _transactions.First().Value.Currency
            };
            return absoluteOperatedVolume;
        }

        /// <summary>
        /// Calcula el total acumulado en concepto de comisiones
        /// </summary>
        /// <returns></returns>
        public AmmountCurrency GetTotalCharges()
        {
            //TODO: Asume que todas las transacciones de Charge tienen la misma divisa
            AmmountCurrency totalCharges = new AmmountCurrency()
            {
                Ammount = _transactions.Sum(x => x.Charge.Ammount),
                Currency = _transactions.First(x => x.Charge.Ammount != null).Charge.Currency
            };
            return totalCharges;
        }

        /// <summary>
        /// Calcula el total acumulado en concepto de valor
        /// </summary>
        /// <returns></returns>
        public AmmountCurrency GetTotalValue()
        {
            //TODO: Asume que todas las transacciones de Value tienen la misma divisa
            AmmountCurrency totalValue = new AmmountCurrency()
            {
                Ammount = _transactions.Sum(x => x.Value.Ammount),
                Currency = _transactions.First(x => x.Value.Ammount != null).Value.Currency
            };
            return totalValue;
        }

        public IEnumerable<IGrouping<string, Transaction>> GetGroupedByProduct()
        {
            IEnumerable<IGrouping<string, Transaction>> groupedTransactions = _transactions.GroupBy(x => x.ISIN);
            return groupedTransactions;
        }

        public IEnumerable<IGrouping<string, Transaction>> GetOpenPositions()
        {
            List<IGrouping<string, Transaction>> openProducts = new List<IGrouping<string, Transaction>>();

            IEnumerable<IGrouping<string, Transaction>> groupedTransactions = GetGroupedByProduct();
            foreach (IGrouping<string, Transaction> productTransactions in groupedTransactions)
            {
                if (productTransactions.Sum(x => x.Quantity) != 0)
                {
                    openProducts.Add(productTransactions);
                }
            }

            return openProducts;
        }

        //TODO: Refactorizar nomenclatura
        //public IEnumerable<IGrouping<string, Transaction>> GetOpenPositionsExact()
        //{
        //    IEnumerable<IGrouping<string, Transaction>> openPositions = GetOpenPositions();

        //    foreach (IGrouping<string, Transaction> position in openPositions)
        //    {
        //        //Ordenar por fecha e ir operando Quantity
        //    }
            
        //}
    }
}
