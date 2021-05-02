using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Models.Renta20
{
    public class Transmisión
    {
        public string Producto { get; set; }
        public int NúmeroTítulos { get; set; }
        public double Beneficio
        {
            get
            {
                return ValorTransmisión + ValorAdquisición;
            }
        }
        public double BeneficioTotal
        {
            get
            {
                return ValorTransmisiónTotal + ValorAdquisiciónTotal;
            }
        }
        public DateTime FechaAdquisición { get; set; }
        public DateTime FechaTransmisión { get; set; }
        public double ValorAdquisición { get; set; }
        public double ValorTransmisión { get; set; }
        public double ValorAdquisiciónTotal { get; set; }
        public double ValorTransmisiónTotal { get; set; }
        public double ValorComisiones { get; set; }
        public TipoTransmisión TipoTransmisión { get; set; }
    }
}
