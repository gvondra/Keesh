using CsvHelper;
using Keesh.Interface.User.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public class CalendarDataProcessor : ICalendarDataProcessor
    {
        private const string EARNINGS_FILE_NAME = "earnings.csv";
        private const int EXPIRATION_HOURS = 12;
        private readonly IFilePurger _filePurger;

        public CalendarDataProcessor(IFilePurger filePurger)
        {
            _filePurger = filePurger;
        }

        public async Task<IEnumerable<EarningsCalendarItem>> GetEarningsData()
        {
            return await Task.Run<IEnumerable<EarningsCalendarItem>>(async () =>
            {
                IEnumerable<EarningsCalendarItem> result = null;
                await PurgeFiles();
                CacheDirectory.CreateCacheDirectory();
                string fileName = CacheDirectory.GetFileName(EARNINGS_FILE_NAME);
                if (File.Exists(fileName))
                {
                    using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (StreamReader reader = new StreamReader(stream, Encoding.Unicode))
                    using (CsvReader csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
                    {
                        result = csvReader.GetRecords<EarningsCalendarItem>().ToList();
                    }
                }
                return result;
            });
        }

        public async Task SaveEarningsData(IEnumerable<EarningsCalendarItem> earningsCalendarItems)
        {
            await Task.Run(() => {
                CacheDirectory.CreateCacheDirectory();
                string fileName = CacheDirectory.GetFileName(EARNINGS_FILE_NAME);
                if (File.Exists(fileName))
                    File.Delete(fileName);
                if (earningsCalendarItems != null)
                {
                    using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                    using (CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture))
                    {
                        csvWriter.WriteRecords(earningsCalendarItems);
                    }
                }
            });
            await PurgeFiles();
        }

        private async Task PurgeFiles()
        {
            DateTime expiration = DateTime.UtcNow.AddHours(-1 * EXPIRATION_HOURS);
            await _filePurger.Purge(expiration, CacheDirectory.GetFileName(EARNINGS_FILE_NAME));
        }
    }
}
