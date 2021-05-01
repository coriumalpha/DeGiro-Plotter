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

            DisplayTransactionsPerDay();

            _reportService.Renta20();
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

        public void DisplayTransactionsPerDay()
        {
            List<DayTransactions> dayTransactions = _reportService.TransactionsPerDay();

            gridTransactionsByDay.DataSource = dayTransactions.Select(x => new { 
                Date = x.Date,
                Total = x.Total.Ammount,
                Value = x.Value.Ammount,
                Charge = x.Value.Ammount,
                TotalCurrency = x.Total.Currency,
                ValueCurrency = x.Value.Currency,
                ChargeCurrency = x.Charge.Currency
            }).ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
