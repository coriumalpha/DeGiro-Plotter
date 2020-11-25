using SJew.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SJew.Business
{
    public class AnalyticsService
    {
        private List<Transaction> _transactions;
        private List<Asset> _portfolio;

        //TODO: Reemplazar por un sistema robusto
        private double _eurUsd = 1.1862;

        public AnalyticsService(List<Transaction> transactions, List<Asset> portfolio)
        {
            _transactions = transactions;
            _portfolio = portfolio;
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
        /// Calcula el total acumulado en concepto de valor (en la moneda de la cuenta de DeGiro, presumiblemente EUR)
        /// </summary>
        /// <param name="fromLocalValue">Realiza el cálculo empleando el tipo de cambio</param>
        /// <returns></returns>
        public AmmountCurrency GetTotalValue(bool fromLocalValue = true)
        {
            if (!fromLocalValue)
            {
                return GetPreCalculatedTotalValue();
            }

            AmmountCurrency totalValue = new AmmountCurrency()
            {
                Ammount = _transactions.Sum(x => x.ForeignExchangeRate.HasValue ? x.LocalValue.Ammount / x.ForeignExchangeRate : x.LocalValue.Ammount),
                Currency = _transactions.First(x => x.Value.Ammount != null).Value.Currency
            };

            return totalValue;
        }

        /// <summary>
        /// Calcula el total acumulado en concepto de valor en la moneda de la cuenta de DeGiro
        /// </summary>
        /// <returns></returns>
        private AmmountCurrency GetPreCalculatedTotalValue()
        {
            AmmountCurrency totalValue = new AmmountCurrency()
            {
                Ammount = _transactions.Sum(x => x.Value.Ammount),
                Currency = _transactions.First(x => x.Value.Ammount != null).Value.Currency
            };

            return totalValue;
        }

        /// <summary>
        /// Obtener el valor total del portfolio en la moneda de la cuenta de DeGiro
        /// </summary>
        /// <returns></returns>
        public AmmountCurrency GetPortfolioTotalValue()
        {
            //TODO: Depende de los tipos de cambio de divisa actuales para poder calcularse.
            //en el futuro podrían listarse los tipos ausentes y legar al usuario la responsabilidad
            //de alimentarlos.

            AmmountCurrency totalValue = new AmmountCurrency()
            {
                Ammount = _portfolio.Where(x => x.Quantity > 0).Sum(x => ToEUR(x.LocalValue)),
                Currency = "EUR"
            };

            return totalValue;
        }

        public double? ToEUR(AmmountCurrency value)
        {
            switch (value.Currency)
            {
                case "EUR":
                    return value.Ammount;
                case "USD":
                    return value.Ammount / _eurUsd;
                default:
                    throw new NotImplementedException("No es posible convertir la divisa " + value.Currency);
            }
        }

        /// <summary>
        /// Obtener la lista de transacciones agrupadas por Producto
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, Transaction>> GetGroupedByProduct()
        {
            IEnumerable<IGrouping<string, Transaction>> groupedTransactions = _transactions.GroupBy(x => x.ISIN);
            return groupedTransactions;
        }

        /// <summary>
        /// Obtener la lista de posiciones abiertas
        /// </summary>
        /// <returns></returns>
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
    }
}
