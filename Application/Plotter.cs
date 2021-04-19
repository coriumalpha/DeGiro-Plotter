using SJew.Business;
using SJew.Entities.Models;
using System;
using System.Collections.Generic;
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

            GetTransactionsPerDay();
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

        public void GetTransactionsPerDay()
        {
            _reportService.TransactionsPerDay();
        }
    }
}
