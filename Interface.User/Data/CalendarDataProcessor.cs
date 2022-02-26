using Keesh.Interface.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public class CalendarDataProcessor : ICalendarDataProcessor
    {
        private const string EARNINGS_FILE_NAME = "earnings.csv";
        private const string IPO_FILE_NAME = "ipo.csv";
        private const int EXPIRATION_HOURS = 12;
        private readonly IFilePurger _filePurger;
        private readonly IDataSerializer _dataSerializer;

        public CalendarDataProcessor(IFilePurger filePurger,
            IDataSerializer dataSerializer)
        {
            _filePurger = filePurger;
            _dataSerializer = dataSerializer;
        }

        public async Task<IEnumerable<EarningsCalendarItem>> GetEarningsData()
        {
            await PurgeFiles();
            CacheDirectory.CreateCacheDirectory();
            string fileName = CacheDirectory.GetFileName(EARNINGS_FILE_NAME);
            return await _dataSerializer.LoadCsvData<EarningsCalendarItem>(fileName);
        }

        public async Task<IEnumerable<IPOCalendarItem>> GetIpoData()
        {
            await PurgeFiles();
            CacheDirectory.CreateCacheDirectory();
            string fileName = CacheDirectory.GetFileName(IPO_FILE_NAME);
            return await _dataSerializer.LoadCsvData<IPOCalendarItem>(fileName);
        }

        public async Task SaveEarningsData(IEnumerable<EarningsCalendarItem> earningsCalendarItems)
        {
            CacheDirectory.CreateCacheDirectory();
            string fileName = CacheDirectory.GetFileName(EARNINGS_FILE_NAME);
            await _dataSerializer.SaveCsvData(fileName, earningsCalendarItems);
            await PurgeFiles();
        }

        public async Task SaveIpoData(IEnumerable<IPOCalendarItem> earningsCalendarItems)
        {
            CacheDirectory.CreateCacheDirectory();
            string fileName = CacheDirectory.GetFileName(IPO_FILE_NAME);
            await _dataSerializer.SaveCsvData(fileName, earningsCalendarItems);
            await PurgeFiles();
        }

        private async Task PurgeFiles()
        {
            DateTime expiration = DateTime.UtcNow.AddHours(-1 * EXPIRATION_HOURS);
            await _filePurger.Purge(expiration, CacheDirectory.GetFileName(EARNINGS_FILE_NAME), CacheDirectory.GetFileName(IPO_FILE_NAME));
        }
    }
}
