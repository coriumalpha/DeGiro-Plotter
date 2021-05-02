using SJew.Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models.Renta20
{
    public class Transacción : Transaction
    {
        public int CierresConsolidados { get; set; }
        public int CierresDisponibles
        {
            get
            {
                return Math.Abs(Quantity) - CierresConsolidados;
            }
        }
        public TipoOperación TipoOperación
        {
            get
            {
                switch (Math.Sign(Quantity))
                {
                    case (1):
                        return TipoOperación.Compra;
                    case (-1):
                        return TipoOperación.Venta;
                    default:
                        throw new Exception();
                }
            }
        }

        public TipoTransacción? TipoTransacción { get; set; }
        public int TítulosSinCierre { get; set; }
    }
}
