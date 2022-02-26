using Keesh.Interface.User.Data;
using Keesh.Interface.User.Framework;
using Keesh.Interface.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Core
{
    public class PortfolioProcessor : IPortfolioProcessor
    {
        private readonly IPortfolioDataProcessor _dataProcessor;

        public PortfolioProcessor(IPortfolioDataProcessor dataProcessor)
        {
            _dataProcessor = dataProcessor;
        }

        public Task<IEnumerable<PortfolioItem>> GetItems(string fileName)
            => _dataProcessor.GetItems(fileName);

        public Task SaveItems(string fileName, IEnumerable<PortfolioItem> items)
            => _dataProcessor.SaveItems(fileName, items);
    }
}
