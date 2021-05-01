using SJew.Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models.Renta20
{
    public class Transacción : Transaction
    {
        public int TítuloInicial { get; set; }
        public int TítuloFinal { get; set; }
        public int CierresConsolidados { get; set; }
        public int CierresDisponibles
        {
            get
            {
                return Math.Abs(Quantity) - CierresConsolidados;
            }
        }
        public List<Transacción> Aperturas { get; set; }
        public List<Transacción> Cierres { get; set; }
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
    }
}
