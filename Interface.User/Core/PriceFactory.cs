using AlphaVantageModels = Keesh.Interface.AlphaVantage.Models;
using AutoMapper;
using Keesh.Interface.AlphaVantage;
using Keesh.Interface.User.Data;
using Keesh.Interface.User.Framework;
using Keesh.Interface.User.Model;
using Keesh.Interface.User.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Core
{
    public class PriceFactory : IPriceFactory
    {
        private readonly IPriceService _priceService;
        private readonly ISettingsFactory _settingsFactory;
        private readonly IPriceDataProcessor _dataProcessor;

        public PriceFactory(IPriceService priceService,
            ISettingsFactory settingsFactory,
            IPriceDataProcessor dataProcessor)
        {
            _priceService = priceService;
            _settingsFactory = settingsFactory;
            _dataProcessor = dataProcessor;
        }

        public async Task<IEnumerable<PriceItem>> GetDaily(string symbol, string apiKey)
        {
            IEnumerable<PriceItem> prices = await _dataProcessor.GetDaily(symbol);
            if (prices == null)
            {
                List<AlphaVantageModels.PriceItem> pricesAlphaVantage = await _priceService.GetDaily(_settingsFactory.CreateAlphaVantage(), symbol, apiKey);
                if (pricesAlphaVantage != null)
                {
                    IMapper mapper = new Mapper(Mapping.Configuration.MapperConfigurationAlphaVantage);
                    prices = pricesAlphaVantage.Select<AlphaVantageModels.PriceItem, PriceItem>(i => mapper.Map<PriceItem>(i));
                    await _dataProcessor.SaveDaily(symbol, prices);
                }
            }
            return prices.OrderByDescending(i => i.Timestamp);
        }
    }
}
