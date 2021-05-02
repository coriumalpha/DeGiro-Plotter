using CsvHelper;
using SJew.Entities.Models.Base;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SJew.Business
{
    public class LoaderService
    {
        private string _transactionsFilePath = @"C:\Users\Corium\Desktop\DegiroPlotterData\Transactions.csv";
        private string _portfolioFilePath = @"C:\Users\Corium\Desktop\DegiroPlotterData\Portfolio.csv";
        public List<Transaction> ReadTransactions()
        {
            FileStream fileStream = new FileStream(_transactionsFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            using (var reader = new StreamReader(fileStream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.Delimiter = ",";
                csv.Configuration.RegisterClassMap<TransactionMapper>();
                IEnumerable<Transaction> records = csv.GetRecords<Transaction>();

                return records.ToList();
            }
        }

        public List<Asset> ReadPortfolio()
        {
            FileStream fileStream = new FileStream(_portfolioFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            using (var reader = new StreamReader(fileStream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<AssetMapper>();
                IEnumerable<Asset> records = csv.GetRecords<Asset>();

                return records.ToList();
            }
        }
    }
}
