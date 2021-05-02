using Entities.Models.Renta20;
using SJew.Business;
using SJew.Entities.Models.Base;
using SJew.Entities.Models.Renta20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GraphicalOperator
{
    public partial class Plotter : Form
    {
        private AnalyticsService _analyticsService;
        private ReportService _reportService;

        public Plotter()
        {
            InitializeComponent();
            InitServices();
        }

        private void InitServices()
        {
            LoaderService loaderService = new LoaderService();
            try
            {
                List<Transaction> transactions = loaderService.ReadTransactions();
                List<Asset> portfolio = loaderService.ReadPortfolio();

                _analyticsService = new AnalyticsService(transactions, portfolio);
                _reportService = new ReportService(transactions, portfolio);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            double totalRevenueDirect = GetTotalRevenue(false);
            string totalRevenueDirectText = totalRevenueDirect.ToString("n2") + " EUR";
            boxLocalRevenue.Text = totalRevenueDirectText;

            double totalRevenue = GetTotalRevenue();
            string totalRevenueText = totalRevenue.ToString("n2") + " EUR";
            boxTotalRevenueLocal.Text = totalRevenueText;

            boxTotalCharges.Text = GetCharges();

            Dictionary<string, List<Transmisión>> reporteRenta = _reportService.Renta20();

            DisplayTransactionsPerDay(reporteRenta);
            Renta20GlobalText.Text = _reportService.ReporteGlobales(reporteRenta);

            //Renta20Text.Text = _reportService.ReporteTransmisionesPorProducto(reporteRenta);
        }

        public string GetCharges()
        {
            AmmountCurrency charges = _analyticsService.GetTotalCharges();

            return charges.Readable;
        }

        public double GetTotalRevenue(bool fromLocalValue = true)
        {
            AmmountCurrency portfolioValue = _analyticsService.GetPortfolioTotalValue();
            AmmountCurrency totalValue = _analyticsService.GetTotalValue(fromLocalValue);
            AmmountCurrency totalCharges = _analyticsService.GetTotalCharges();

            double totalRevenue = (portfolioValue.Ammount + totalValue.Ammount + totalCharges.Ammount) ?? 0;

            return totalRevenue;
        }

        public void DisplayTransactionsPerDay(Dictionary<string, List<Transmisión>> renta20)
        {
            List<Transmisión> transmisiones = renta20.Values.SelectMany(x => x).ToList();

            gridTransmisiones.DataSource = transmisiones.OrderBy(x => x.FechaAdquisición).Select(x => new { 
                Producto = x.Producto,
                Beneficio = x.BeneficioTotal.ToString("0.##"),
                FechaAdquisición = x.FechaAdquisición,
                FechaTransmisión = x.FechaTransmisión,
                Títulos = x.NúmeroTítulos,
                Comisiones = x.ValorComisiones.ToString("0.##"),
                ValorAquisición = x.ValorAdquisiciónTotal.ToString("0.##"),
                ValorTransmisión = x.ValorTransmisiónTotal.ToString("0.##")
            }).ToList();

            gridTransmisiones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
