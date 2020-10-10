using CsvHelper;
using SJew.Entities.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SJew.Business
{
    public class LoaderService
    {
        private string _filePath = @"C:\Users\Corium\Desktop\Transactions.csv";
        public List<Transaction> ReadTransactions()
        {
            FileStream fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            using (var reader = new StreamReader(fileStream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<TransactionMapper>();
                IEnumerable<Transaction> records = csv.GetRecords<Transaction>();

                return records.ToList();
            }
        }
    }
}
