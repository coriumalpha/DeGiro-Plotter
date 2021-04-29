using SJew.Entities.Models.Base;
using System;
using URF.Core.EF.Trackable;

namespace SJew.Entities.Models.Renta20
{
    public partial class DayTransactions : Entity
    {
        public DateTime Date { get; set; }
        public AmmountCurrency Value { get; set; }
        public AmmountCurrency Charge { get; set; }
        public AmmountCurrency Total { get; set; }
    }
}