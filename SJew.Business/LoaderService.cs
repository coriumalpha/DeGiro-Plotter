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
        public List<Transaction> ReadTransactions()
        {
            using (var reader = new StreamReader(@"C:\Users\Corium\Desktop\Transactions.csv"))
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
