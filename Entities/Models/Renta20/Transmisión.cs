using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Models.Renta20
{
    public class Transmisión
    {
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
        public double ValorAdquisición { get; set; } //En largo, negativo
        public double ValorTransmisión { get; set; } //En largo, positivo
        public double ValorAdquisiciónTotal { get; set; } //En largo, negativo
        public double ValorTransmisiónTotal { get; set; } //En largo, positivo
        public double ValorComisiones { get; set; }
    }
}
