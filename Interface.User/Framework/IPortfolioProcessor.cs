using Keesh.Interface.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Framework
{
    public interface IPortfolioProcessor
    {
        Task<IEnumerable<PortfolioItem>> GetItems(string fileName);
        Task SaveItems(string fileName, IEnumerable<PortfolioItem> items);
    }
}
