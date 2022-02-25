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
using System.Threading.Tasks;
using System.Web;

namespace Keesh.Interface.AlphaVantage
{
    public class CalendarService : ICalendarService
    {
        private readonly IService _service;

        public CalendarService(IService service)
        {
            _service = service;
        }

        public async Task<List<EarningsCalendarItem>> GetEarningsCalendar(ISettings settings, string apiKey)
        {
            UriBuilder uriBuilder = new UriBuilder(settings.BaseAddress);
            uriBuilder.Path = "query";
            uriBuilder.Query = $"function=EARNINGS_CALENDAR&horizon=3month&apikey={HttpUtility.UrlEncode(apiKey)}";
            IRequest request = _service.CreateRequest(uriBuilder.Uri, HttpMethod.Get);
            IResponse response = await _service.Send(request);
            using (StreamReader reader = new StreamReader(await response.Message.Content.ReadAsStreamAsync()))
            using (CsvReader csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                return (csvReader.GetRecords<EarningsCalendarItem>()).ToList();
            }
        }
    }
}
