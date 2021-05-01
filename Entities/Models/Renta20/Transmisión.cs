using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Models.Renta20
{
    public class Transmisión
    {
        public int CantidadTítulos
        {
            get
            {
                return AperturasPorTransacción.Sum(x => x.Value);
            }
        }
        public double Beneficio
        {
            get
            {
                return ValorTransmisión + ValorAdquisición;
            }
        }
        public DateTime FechaAdquisición { get; set; }
        public DateTime FechaTransmisión { get; set; }
        public double ValorAdquisición { get; set; }
        public double ValorTransmisión { get; set; }
        public Dictionary<Transacción, int> AperturasPorTransacción { get; set; }
        public Dictionary<Transacción, int> CierresPorTransacción { get; set; }
    }
}
