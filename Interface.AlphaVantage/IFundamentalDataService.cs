using Keesh.Interface.AlphaVantage.Models;
using System.Threading.Tasks;

namespace Keesh.Interface.AlphaVantage
{
    public interface IFundamentalDataService
    {
        Task<CompanyOverview> GetCompanyOverview(ISettings settings, string symbol, string apiKey);
    }
}
