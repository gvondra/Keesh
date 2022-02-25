using BrassLoon.RestClient;
using Keesh.Interface.AlphaVantage.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Keesh.Interface.AlphaVantage
{
    public class FundamentalDataService : IFundamentalDataService
    {
        private readonly IService _service;

        public FundamentalDataService(IService service)
        {
            _service = service;
        }

        public async Task<CompanyOverview> GetCompanyOverview(ISettings settings, string symbol, string apiKey)
        {
            UriBuilder uriBuilder = new UriBuilder(settings.BaseAddress);
            uriBuilder.Path = "query";
            uriBuilder.Query = $"function=OVERVIEW&symbol={HttpUtility.UrlEncode(symbol)}&apikey={HttpUtility.UrlEncode(apiKey)}";
            IRequest request = _service.CreateRequest(uriBuilder.Uri, HttpMethod.Get);
            IResponse<CompanyOverview> response = await _service.Send<CompanyOverview>(request);
            return response.Value;
        }
    }
}
