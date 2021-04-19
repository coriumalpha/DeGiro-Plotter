using System;
using URF.Core.EF.Trackable;

namespace SJew.Entities.Models
{
    /// <summary>
    /// Transacción proviniente del histórico
    /// </summary>
    public partial class Transaction : Entity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Product { get; set; }
        public string ISIN { get; set; }
        public string ExchangeMarket { get; set; }
        public string ExecutionCenter { get; set; }
        public int Quantity { get; set; }
        public AmmountCurrency Price { get; set; }
        public AmmountCurrency LocalValue { get; set; }
        public AmmountCurrency Value { get; set; }
        public double? ForeignExchangeRate { get; set; }
        public AmmountCurrency Charge { get; set; }
        public AmmountCurrency Total { get; set; }
    }
}