using Keesh.Interface.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public class PriceDataProcessor : IPriceDataProcessor
    {
        private const string DAILY_INDEX_FILE_NAME = "daily-prices.json";
        private const int EXPIRATION_HOURS = 8;
        private readonly IDataSerializer _dataSerializer;
        private readonly IFilePurger _filePurger;

        public PriceDataProcessor(IFilePurger filePurger,
            IDataSerializer dataSerializer)
        {
            _filePurger = filePurger;
            _dataSerializer = dataSerializer;   
        }

        public async Task<IEnumerable<PriceItem>> GetDaily(string symbol)
        {
            CacheDirectory.CreateCacheDirectory();
            IEnumerable<PriceItem> result = null;
            string indexFileName = CacheDirectory.GetFileName(DAILY_INDEX_FILE_NAME);
            Dictionary<string, string> index = await _dataSerializer.LoadJsonData<Dictionary<string, string>>(indexFileName);
            if (index != null && index.ContainsKey(symbol))
            {
                await PurgeFiles(index);
                if (index != null && index.ContainsKey(symbol))
                {
                    string fileName = GetDataFileName(index[symbol]);
                    result = await _dataSerializer.LoadCsvData<PriceItem>(fileName);
                }
            }
            return result;
        }

        public async Task SaveDaily(string symbol, IEnumerable<PriceItem> items)
        {
            CacheDirectory.CreateCacheDirectory();
            string indexFileName = CacheDirectory.GetFileName(DAILY_INDEX_FILE_NAME);
            Dictionary<string, string> index = await _dataSerializer.LoadJsonData<Dictionary<string, string>>(indexFileName);
            if (index == null)
                index = new Dictionary<string, string>();
            if (!index.ContainsKey(symbol))
            {
                index[symbol] = Guid.NewGuid().ToString("N");
                await _dataSerializer.SaveJsonData(indexFileName, index);
            }
            await _dataSerializer.SaveCsvData(GetDataFileName(index[symbol]), items);
            await PurgeFiles(index);
        }

        private async Task PurgeFiles(Dictionary<string, string> index)
        {
            DateTime expiration = DateTime.UtcNow.AddHours(-1 * EXPIRATION_HOURS);
            await _filePurger.Purge(expiration, index.Select(kv => GetDataFileName(kv.Value)));
        }

        private string GetDataFileName(string guid)
            => CacheDirectory.GetFileName($"{guid}.csv");
    }
}
