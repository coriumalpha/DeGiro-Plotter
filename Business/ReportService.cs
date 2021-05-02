using Entities.Models.Renta20;
using SJew.Entities.Models.Base;
using SJew.Entities.Models.Renta20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJew.Business
{
    public class ReportService
    {
        private List<Transaction> _transactions;
        private List<Asset> _portfolio;

        private List<Transacción> _transacciones;

        public ReportService(List<Transaction> transactions, List<Asset> portfolio)
        {
            _transactions = transactions;
            _portfolio = portfolio;

            _transacciones = transactions.Select(x => new Transacción()
            {
                Id = x.Id,
                Date = x.Date,
                Product = x.Product,
                ISIN = x.ISIN,
                ExchangeMarket = x.ExchangeMarket,
                ExecutionCenter = x.ExecutionCenter,
                Quantity = x.Quantity,
                Price = x.Price,
                LocalValue = x.LocalValue,
                Value = x.Value,
                ForeignExchangeRate = x.ForeignExchangeRate,
                Charge = x.Charge,
                Total = x.Total
            }).ToList();
        }

        public List<DayTransactions> TransactionsPerDay()
        {
            Dictionary<DateTime, List<Transaction>> transactionsPerDay = _transactions
                .OrderBy(x => x.Date)
                .GroupBy(x => x.Date.Date)
                .ToDictionary(x => x.Key, x => x.ToList());

            List<DayTransactions> dayTransactions = transactionsPerDay
                .Select(x => new DayTransactions()
                {
                    Date = x.Key,
                    Value = new AmmountCurrency(x.Value.Sum(x => x.Value.Ammount), x.Value.First().Value.Currency),
                    Charge = new AmmountCurrency(x.Value.Sum(x => x.Charge.Ammount), x.Value.First().Charge.Currency),
                    Total = new AmmountCurrency(x.Value.Sum(x => x.Total.Ammount), x.Value.First().Total.Currency)
                }).ToList();

            return dayTransactions;
        }

        public Dictionary<string, List<Transmisión>> Renta20()
        {
            Dictionary<string, List<Transacción>> transaccionesPorProducto = _transacciones
                .GroupBy(x => x.Product)
                .ToDictionary(x => x.Key, x => x.ToList());

            Dictionary<string, List<Transmisión>> transmisionesPorProducto = new Dictionary<string, List<Transmisión>>();

            foreach (KeyValuePair<string, List<Transacción>> grupoProducto in transaccionesPorProducto)
            {
                List<Transmisión> transmisiones = ObtenerTransmisionesProducto(grupoProducto.Value);
                transmisionesPorProducto.Add(grupoProducto.Key, transmisiones);
            }

            var sinCerrar = transaccionesPorProducto.Values.SelectMany(x => x).Where(x => x.CierresDisponibles != 0 && x.TipoTransacción == TipoTransacción.Cierre);

            return transmisionesPorProducto;
        }

        private List<Transmisión> ObtenerTransmisionesProducto(List<Transacción> transacciones)
        {
            List<Transmisión> transmisiones = new List<Transmisión>();

            List<Transacción> transaccionesPorFecha = transacciones.OrderBy(x => x.Date).ToList();

            int títulosPendientesDeCierre = 0;
            foreach (Transacción transacción in transaccionesPorFecha)
            {
                if (transacción.TipoTransacción == null)
                {
                    if (Math.Abs(títulosPendientesDeCierre + transacción.Quantity) > Math.Abs(títulosPendientesDeCierre))
                    {
                        //Continúa siendo apertura
                        transacción.TipoTransacción = TipoTransacción.Apertura;
                    }
                    else
                    {
                        transacción.TipoTransacción = TipoTransacción.Cierre;
                    }
                }

                if (transacción.TipoTransacción == TipoTransacción.Cierre)
                {
                    continue;
                }              

                títulosPendientesDeCierre += transacción.Quantity;

                IEnumerable<Transacción> potencialesCierres = transaccionesPorFecha.Where(x => x.TipoOperación != transacción.TipoOperación && x.CierresDisponibles > 0 && x.TipoTransacción != TipoTransacción.Apertura);

                foreach (Transacción cierre in potencialesCierres)
                {
                    if (títulosPendientesDeCierre == 0)
                    {
                        break;
                    }

                    cierre.TipoTransacción = TipoTransacción.Cierre;

                    if (Math.Abs(títulosPendientesDeCierre) >= cierre.CierresDisponibles)
                    {
                        //Uso de todos los cierres disponibles
                        int títulosCerrados = cierre.CierresDisponibles;
                        títulosPendientesDeCierre += (títulosCerrados * Math.Sign(cierre.Quantity));
                        cierre.CierresConsolidados += títulosCerrados;

                        transmisiones.Add(CrearTransmisión(transacción, cierre, títulosCerrados));
                        continue;
                    }
                    else
                    {
                        int títulosCerrados = Math.Abs(títulosPendientesDeCierre);
                        cierre.CierresConsolidados += títulosCerrados;
                        títulosPendientesDeCierre = 0;

                        transmisiones.Add(CrearTransmisión(transacción, cierre, títulosCerrados));

                        break;
                    }
                }
            }

            return transmisiones;
        }

        private Transmisión CrearTransmisión(Transacción apertura, Transacción cierre, int títulosCerrados)
        {
            double valorComisionesApertura = ((apertura.Charge.Ammount ?? 0) / Math.Abs(apertura.Quantity)) * títulosCerrados;
            double valorComisionesCierre = ((cierre.Charge.Ammount ?? 0) / Math.Abs(cierre.Quantity)) * títulosCerrados;

            if (apertura.Charge.Currency != "EUR" && !String.IsNullOrEmpty(apertura.Charge.Currency)) throw new Exception();
            if (cierre.Charge.Currency != "EUR" && !String.IsNullOrEmpty(cierre.Charge.Currency)) throw new Exception();

            return new Transmisión()
            {
                FechaAdquisición = apertura.Date,
                FechaTransmisión = cierre.Date,
                ValorAdquisición = (apertura.Value.Ammount.Value / Math.Abs(apertura.Quantity)) * títulosCerrados,
                ValorTransmisión = (cierre.Value.Ammount.Value / Math.Abs(cierre.Quantity)) * títulosCerrados,
                ValorAdquisiciónTotal = (apertura.Total.Ammount.Value / Math.Abs(apertura.Quantity)) * títulosCerrados,
                ValorTransmisiónTotal = (cierre.Total.Ammount.Value / Math.Abs(cierre.Quantity)) * títulosCerrados,
                ValorComisiones = valorComisionesApertura + valorComisionesCierre,
                NúmeroTítulos = títulosCerrados
            };
        }

        public string ReporteTransmisionesPorProducto(Dictionary<string, List<Transmisión>> transmisionesPorProducto)
        {
            StringBuilder resultados = new StringBuilder();

            foreach (KeyValuePair<string, List<Transmisión>> transmisionesProducto in transmisionesPorProducto.OrderBy(x => x.Key))
            {
                resultados.AppendFormat("{0}: {1} ({2}) [Com. {3}] \r\n", transmisionesProducto.Key, transmisionesProducto.Value.Sum(x => x.BeneficioTotal), transmisionesProducto.Value.Count(), transmisionesProducto.Value.Sum(x => x.ValorComisiones));
            }

            return resultados.ToString();
        }

        public string ReporteGlobales(Dictionary<string, List<Transmisión>> transmisionesPorProducto)
        {
            StringBuilder reporte = new StringBuilder();

            double beneficio = transmisionesPorProducto.Values.SelectMany(x => x).Where(x => x.Beneficio > 0).Sum(x => x.Beneficio);
            double pérdida = transmisionesPorProducto.Values.SelectMany(x => x).Where(x => x.Beneficio < 0).Sum(x => x.Beneficio);
            double beneficioTotal = transmisionesPorProducto.Values.SelectMany(x => x).Where(x => x.BeneficioTotal > 0).Sum(x => x.BeneficioTotal);
            double pérdidaTotal = transmisionesPorProducto.Values.SelectMany(x => x).Where(x => x.BeneficioTotal < 0).Sum(x => x.BeneficioTotal);
            double valorComisiones = transmisionesPorProducto.Values.SelectMany(x => x).Sum(x => x.ValorComisiones);


            reporte.AppendLine(string.Format("Valor comisiones: {0}", valorComisiones));
            reporte.AppendLine(string.Format("Beneficio: {0}", beneficio));
            reporte.AppendLine(string.Format("Beneficio total: {0}", beneficioTotal));
            reporte.AppendLine(string.Format("Pérdida: {0}", pérdida));
            reporte.AppendLine(string.Format("Pérdida total: {0}", pérdidaTotal));
            reporte.AppendLine(string.Format("Diferencia: {0}", beneficio + pérdida));
            reporte.AppendLine(string.Format("Diferencia totales: {0}", beneficioTotal + pérdidaTotal));
            reporte.AppendLine(string.Format("Comisiones (estimado por diferencia): {0}", (beneficio + pérdida) - (beneficioTotal + pérdidaTotal)));

            return reporte.ToString();
        }
    }
}
