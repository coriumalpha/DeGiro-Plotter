using System;
using URF.Core.EF.Trackable;

namespace SJew.Entities.Models.Base
{
    /// <summary>
    /// Activo presente en el portfolio actual
    /// </summary>
    public partial class Asset : Entity
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
        public string SymbolOrISIN { get; set; }
        public double? Quantity { get; set; }
        public AmmountCurrency LastPrice { get; set; }
        public AmmountCurrency LocalValue { get; set; }
        public AmmountCurrency EURValue { get; set; }

    }
}