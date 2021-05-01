using Entities.Models.Renta20;
using SJew.Entities.Models.Base;
using SJew.Entities.Models.Renta20;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Renta20()
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

            return;
        }

        private List<Transmisión> ObtenerTransmisionesProducto(List<Transacción> transacciones)
        {
            List<Transmisión> transmisiones = new List<Transmisión>();
            List<Transacción> transaccionesPorFecha = transacciones.OrderBy(x => x.Date).ToList();

            TipoTransacción tipoApertura = transaccionesPorFecha.First().TipoTransacción;

            TipoTransacción últimoTipo = transaccionesPorFecha.First().TipoTransacción;

            List<Transacción> cierres = new List<Transacción>();

            int potencialesAperturas = transaccionesPorFecha.Count();

            for (int i = 0; i < potencialesAperturas; i++)
            {
                Transacción transacción = transaccionesPorFecha.Where(x => !cierres.Contains(x)).ElementAt(i);

                if (transacción.TipoTransacción == TipoTransacción.Venta)
                {
                    throw new Exception();
                }

                int cierresConsolidados = 0;

                var potencialesCierres = transaccionesPorFecha.Where(x => x.TipoTransacción != transacción.TipoTransacción && x.CierresDisponibles > 0);
                for (int j = 0; j < potencialesCierres.Count(); j++)
                {
                    //TODO: El acceso por índice se está desfasando
                    Transacción cierre = potencialesCierres.ElementAt(j);

                    int pendienteConsolidar = Math.Abs(transacción.Quantity) - cierresConsolidados;
                    if (pendienteConsolidar <= 0)
                    {
                        //Todos los cierres consolidados
                        cierresConsolidados = 0;
                        break;
                    }
                    else
                    {
                        cierres.Add(cierre);
                        potencialesAperturas = transaccionesPorFecha.Where(x => !cierres.Contains(x)).Count();

                        //Añadir cierre y continuar
                        int títulosCerrados = (Math.Abs(cierre.CierresDisponibles) > pendienteConsolidar) ? pendienteConsolidar : Math.Abs(cierre.CierresDisponibles);
                        cierresConsolidados += títulosCerrados;
                        cierre.CierresConsolidados += títulosCerrados;
                        continue;
                    }
                }
            }

            cierres = cierres.Distinct().OrderBy(x => x.Date).ToList();
            List<Transacción> aperturas = transaccionesPorFecha.Except(cierres).ToList();

            AsignarPosiciónTítulos(aperturas);
            AsignarPosiciónTítulos(cierres);

            foreach (Transacción apertura in aperturas)
            {
                apertura.Cierres = cierres.Where(x => x.TítuloInicial >= apertura.TítuloInicial && x.TítuloInicial <= apertura.TítuloFinal).ToList();
            }

            foreach (Transacción cierre in cierres)
            {
                cierre.Aperturas = aperturas.Where(x => x.TítuloInicial >= cierre.TítuloInicial && x.TítuloInicial <= cierre.TítuloFinal).ToList();
            }

            foreach (Transacción cierre in cierres)
            {
                foreach (Transacción apertura in cierre.Aperturas)
                {
                    int títulosTransmitidos = (cierre.TítuloFinal <= apertura.TítuloFinal) ? cierre.TítuloFinal - apertura.TítuloInicial + 1 : apertura.TítuloFinal - apertura.TítuloInicial + 1;

                    if (títulosTransmitidos == 0)
                    {
                        break;
                    }

                    Transmisión transmisión = new Transmisión()
                    { 
                        FechaAdquisición = apertura.Date,
                        FechaTransmisión = cierre.Date,
                        ValorAdquisición = (apertura.Total.Ammount.Value / Math.Abs(apertura.Quantity)) * títulosTransmitidos,
                        ValorTransmisión = (cierre.Total.Ammount.Value / Math.Abs(cierre.Quantity)) * títulosTransmitidos,
                        AperturasPorTransacción = new Dictionary<Transacción, int>() { { apertura, títulosTransmitidos } },
                        CierresPorTransacción = new Dictionary<Transacción, int>() { { cierre, títulosTransmitidos } }
                    };

                    transmisiones.Add(transmisión);
                }
            }

            return transmisiones;
        }

        private void AsignarPosiciónTítulos(List<Transacción> transacciones)
        {
            int últimoAsignado = -1;
            foreach (Transacción transacción in transacciones.OrderBy(x => x.Date))
            {
                transacción.TítuloInicial = últimoAsignado + 1;
                transacción.TítuloFinal = transacción.TítuloInicial + Math.Abs(transacción.Quantity) - 1;
                últimoAsignado = transacción.TítuloFinal;
            }
        }
    }
}
