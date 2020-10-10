using System;
using System.Collections.Generic;
using System.Text;
using URF.Core.EF.Trackable;

namespace SJew.Entities.Models
{
    public class AmmountCurrency : Entity
    {
        public AmmountCurrency()
        {

        }
        public AmmountCurrency(double? ammount, string currency)
        {
            Ammount = ammount;
            Currency = currency;
        }
        public double? Ammount { get; set; }
        public string Currency { get; set; }

        public string Readable
        {
            get
            {
                return String.Join(" ", Ammount.ToString(), Currency);
            }
        }
    }
}
