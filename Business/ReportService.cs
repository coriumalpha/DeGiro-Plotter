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

        public string Renta20()
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

            double balanceFirst = transmisionesPorProducto.Values.First().Sum(x => x.Beneficio);
            double balance = transmisionesPorProducto.Values.Select(x => x.Sum(x => x.Beneficio)).Sum();

            StringBuilder resultados = new StringBuilder();

            foreach (KeyValuePair<string, List<Transmisión>> transmisionesProducto in transmisionesPorProducto.OrderBy(x => x.Key))
            {
                resultados.AppendFormat("{0}: {1} ({2}) \r\n", transmisionesProducto.Key, transmisionesProducto.Value.Sum(x => x.Beneficio), transmisionesProducto.Value.Count());
            }

            double beneficioTotal = transmisionesPorProducto.Values.SelectMany(x => x).Where(x => x.Beneficio > 0).Sum(x => x.Beneficio);
            double pérdidaTotal = transmisionesPorProducto.Values.SelectMany(x => x).Where(x => x.Beneficio < 0).Sum(x => x.Beneficio);

            return resultados.ToString();
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

                IEnumerable<Transacción> potencialesCierres = transaccionesPorFecha.Where(x => x.TipoOperación != transacción.TipoOperación && x.CierresDisponibles > 0);

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

                        transmisiones.Add(CrearTransmisión(transmisiones, transacción, cierre, títulosCerrados));
                        continue;
                    }
                    else
                    {
                        int títulosCerrados = Math.Abs(títulosPendientesDeCierre);
                        cierre.CierresConsolidados += títulosCerrados;
                        títulosPendientesDeCierre = 0;

                        transmisiones.Add(CrearTransmisión(transmisiones, transacción, cierre, títulosCerrados));

                        break;
                    }
                }
            }

            return transmisiones;
        }

        private Transmisión CrearTransmisión(List<Transmisión> transmisiones, Transacción apertura, Transacción cierre, int títulosCerrados)
        {
            //double valorAdquisición = 

            return new Transmisión()
            {
                FechaAdquisición = apertura.Date,
                FechaTransmisión = cierre.Date,
                ValorAdquisición = (apertura.Value.Ammount.Value / Math.Abs(apertura.Quantity)) * títulosCerrados,
                ValorTransmisión = (cierre.Value.Ammount.Value / Math.Abs(cierre.Quantity)) * títulosCerrados,
                ValorAdquisiciónTotal = (apertura.Total.Ammount.Value / Math.Abs(apertura.Quantity)) * títulosCerrados,
                ValorTransmisiónTotal = (cierre.Total.Ammount.Value / Math.Abs(cierre.Quantity)) * títulosCerrados,
                NúmeroTítulos = títulosCerrados
            };
        }
    }
}
