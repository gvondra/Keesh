using Keesh.Interface.AlphaVantage.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.AlphaVantage
{
    public interface IPriceService
    {
        Task<List<PriceItem>> GetDaily(ISettings settings, string symbol, string apiKey, bool fullResultSet = false);
    }
}
