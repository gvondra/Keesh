using BrassLoon.RestClient;
using CsvHelper;
using Keesh.Interface.AlphaVantage.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Keesh.Interface.AlphaVantage
{
    public class PriceService : IPriceService
    {
        private readonly static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly IService _service;

        public PriceService(IService service)
        {
            _service = service;
        }

        public async Task<List<PriceItem>> GetDaily(ISettings settings, string symbol, string apiKey, bool fullResultSet = false)
        {
            await _semaphore.WaitAsync();
            try
            {
                // the alpha vantage web site is rate limited as 5 calls per minute
                Thread.Sleep(13000);
                string outputSize = "full";
                if (!fullResultSet)
                    outputSize = "compact";
                UriBuilder uriBuilder = new UriBuilder(settings.BaseAddress);
                uriBuilder.Path = "query";
                uriBuilder.Query = $"function=TIME_SERIES_DAILY&symbol={HttpUtility.UrlEncode(symbol)}&outputSize={outputSize}&datatype=csv&apikey={HttpUtility.UrlEncode(apiKey)}";
                IRequest request = _service.CreateRequest(uriBuilder.Uri, HttpMethod.Get);
                IResponse response = await _service.Send(request);
                //System.Diagnostics.Debug.WriteLine(await response.Message.Content.ReadAsStringAsync());
                using (StreamReader reader = new StreamReader(await response.Message.Content.ReadAsStreamAsync()))
                using (CsvReader csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    return (csvReader.GetRecords<PriceItem>()).ToList();
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
