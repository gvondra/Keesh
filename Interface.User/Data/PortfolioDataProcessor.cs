using Keesh.Interface.User.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public class PortfolioDataProcessor : IPortfolioDataProcessor
    {
        private readonly IDataSerializer _dataSerializer;

        public PortfolioDataProcessor(IDataSerializer dataSerializer)
        {
            _dataSerializer = dataSerializer;
        }

        public async Task<IEnumerable<PortfolioItem>> GetItems(string fileName)
        {            
            List<PortfolioItem> result = null;
            if (File.Exists(fileName))
            {
                result = await _dataSerializer.LoadCsvData<PortfolioItem>(fileName);
            }
            return result;
        }

        public async Task SaveItems(string fileName, IEnumerable<PortfolioItem> items)
        {
            await _dataSerializer.SaveCsvData<PortfolioItem>(fileName, items);
        }
    }
}
