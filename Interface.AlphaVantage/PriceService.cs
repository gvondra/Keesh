using BrassLoon.RestClient;
using CsvHelper;
using Keesh.Interface.AlphaVantage.Models;
using Polly;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Keesh.Interface.AlphaVantage
{
    public class PriceService : IPriceService
    {
        private static readonly Policy _rateLimit = Policy.RateLimit(5, TimeSpan.FromSeconds(60));
        private readonly IService _service;

        public PriceService(IService service)
        {
            _service = service;
        }

        public Task<List<PriceItem>> GetDaily(ISettings settings, string symbol, string apiKey, bool fullResultSet = false)
        {
            if (settings.EnableRateLimit)
                return _rateLimit.Execute(() => InnerGetDaily(settings, symbol, apiKey, fullResultSet));
            else
                return InnerGetDaily(settings, symbol, apiKey, fullResultSet);
        }

        private async Task<List<PriceItem>> InnerGetDaily(ISettings settings, string symbol, string apiKey, bool fullResultSet = false)
        {
            string outputSize = "full";
            if (!fullResultSet)
                outputSize = "compact";
            UriBuilder uriBuilder = new UriBuilder(settings.BaseAddress);
            uriBuilder.Path = "query";
            uriBuilder.Query = $"function=TIME_SERIES_DAILY_Adjusted&symbol={HttpUtility.UrlEncode(symbol)}&outputSize={outputSize}&datatype=csv&apikey={HttpUtility.UrlEncode(apiKey)}";
            IRequest request = _service.CreateRequest(uriBuilder.Uri, HttpMethod.Get);
            IResponse response = await _service.Send(request);
            //System.Diagnostics.Debug.WriteLine(await response.Message.Content.ReadAsStringAsync());
            using (StreamReader reader = new StreamReader(await response.Message.Content.ReadAsStreamAsync()))
            using (CsvReader csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                return (csvReader.GetRecords<PriceItem>()).ToList();
            }
        }
    }
}
