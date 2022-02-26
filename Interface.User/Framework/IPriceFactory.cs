using Keesh.Interface.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Framework
{
    public interface IPriceFactory
    {
        Task<IEnumerable<PriceItem>> GetDaily(string symbol, string apiKey);
    }
}
