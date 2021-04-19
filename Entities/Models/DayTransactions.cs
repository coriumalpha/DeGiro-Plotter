using System;
using URF.Core.EF.Trackable;

namespace SJew.Entities.Models
{
    public partial class DayTransactions : Entity
    {
        public DateTime Date { get; set; }
        public AmmountCurrency Value { get; set; }
        public AmmountCurrency Charge { get; set; }
        public AmmountCurrency Total { get; set; }
    }
}