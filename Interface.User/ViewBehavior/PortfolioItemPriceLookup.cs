using Keesh.Interface.User.Framework;
using Keesh.Interface.User.Settings;
using Keesh.Interface.User.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Keesh.Interface.User.ViewBehavior
{
    public class PortfolioItemPriceLookup
    {
        private readonly PortfolioItemVM _portfolioItem;
        private readonly IPriceFactory _priceFactory;
        private readonly IApiKeyProcessor _apiKeyProcessor;
        private readonly ISettingsFactory _settingsFactory;

        public PortfolioItemPriceLookup(PortfolioItemVM portfolioItemVM,
            IPriceFactory priceFactory,
            IApiKeyProcessor apiKeyProcessor,
            ISettingsFactory settingsFactory)
        {
            _priceFactory = priceFactory;
            _apiKeyProcessor = apiKeyProcessor;
            _settingsFactory = settingsFactory;
            _portfolioItem = portfolioItemVM;
            portfolioItemVM.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PortfolioItemVM.Symbol):
                    UpdatePrice();
                    break;
            }
        }

        private void UpdatePrice()
        {
            _portfolioItem.PriceBackground = Brushes.Goldenrod;
            Task.Run(() => GetPrice(_portfolioItem.Symbol))
                .ContinueWith(UpdatePriceCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task<Model.PriceItem> GetPrice(string symbol)
        {            
            Model.ApiKey apiKey = _apiKeyProcessor.GetApiKey(_settingsFactory.Create());
            if (!string.IsNullOrEmpty(apiKey?.Key))
                return (await _priceFactory.GetDaily(symbol, apiKey.Key)).OrderByDescending(i => i.Timestamp).FirstOrDefault();
            else
                return null;
        }

        private async Task UpdatePriceCallback(Task<Model.PriceItem> task, object state)
        {
            Model.PriceItem priceItem = await task;
            _portfolioItem.PriceBackground = Brushes.White;
            if (priceItem != null)
            {
                _portfolioItem.Price = priceItem.Close;
            }
        }
    }
}
