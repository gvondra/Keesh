﻿using Keesh.Interface.User.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public class CompanyOverviewDataProcessor : ICompanyOverviewDataProcessor
    {
        private const int EXPIRATION_HOURS = 8;
        private const string INDEX_FILE_NAME = "CompanyOverview.json";
        private readonly IDataSerializer _dataSerializer;
        private readonly IFilePurger _filePurger;

        public CompanyOverviewDataProcessor(IDataSerializer dataSerializer,
            IFilePurger filePurger)
        {
            _dataSerializer = dataSerializer;
            _filePurger = filePurger;
        }

        public async Task<CompanyOverview> Get(string symbol)
        {
            CompanyOverview result = null;
            string indexFileName = GetIndexFileName();
            Dictionary<string, string> index = await _dataSerializer.LoadJsonData<Dictionary<string, string>>(indexFileName);
            await PurgeFiles(index);
            if (index != null && index.ContainsKey(symbol))
            {
                result = await _dataSerializer.LoadJsonData<CompanyOverview>(GetDataFileName(index[symbol]));
            }
            return result;
        }

        public async Task Save(string symbol, CompanyOverview data)
        {
            CacheDirectory.CreateCacheDirectory();
            string indexFileName = GetIndexFileName();
            Dictionary<string, string> index = await _dataSerializer.LoadJsonData<Dictionary<string, string>>(indexFileName);
            if (index == null)
                index = new Dictionary<string, string>();
            if (!index.ContainsKey(symbol))
            {
                index[symbol] = Guid.NewGuid().ToString("N");
                await _dataSerializer.SaveJsonData(indexFileName, index);
            }
            await _dataSerializer.SaveJsonData(GetDataFileName(index[symbol]), data);
            await PurgeFiles(index);
        }

        private async Task PurgeFiles(Dictionary<string, string> index)
        {
            if (index != null)
            {
                DateTime expiration = DateTime.UtcNow.AddHours(-1 * EXPIRATION_HOURS);
                await _filePurger.Purge(expiration, index.Select(kv => GetDataFileName(kv.Value)));
            }
        }

        private string GetDataFileName(string guid)
            => CacheDirectory.GetFileName($"{guid}.json");

        private string GetIndexFileName()
            => CacheDirectory.GetFileName(INDEX_FILE_NAME);
    }
}
