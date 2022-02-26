using Keesh.Interface.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public interface IPriceDataProcessor
    {
        Task<IEnumerable<PriceItem>> GetDaily(string symbol);
        Task SaveDaily(string symbol, IEnumerable<PriceItem> items);
    }
}
