using Keesh.Interface.User.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.InputValidator
{
    public class PriceHistoryValidator
    {
        private const string IS_REQUIRED = " is required";
        private readonly Dictionary<string, Dictionary<string, string>> _messages = new Dictionary<string, Dictionary<string, string>>
        {
            {
                nameof(PriceHistoryVM.TickerSymbol),
                new Dictionary<string, string>
                {
                    { "Required", IS_REQUIRED }
                }
            }
        };
        private readonly PriceHistoryVM _priceHistoryVM;

        public PriceHistoryValidator(PriceHistoryVM priceHistoryVM)
        {
            _priceHistoryVM = priceHistoryVM;
            priceHistoryVM.PropertyChanged += OnProperyChanged;
        }

        public void OnProperyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CompanyOverviewVM.TickerSymbol):
                    if (string.IsNullOrEmpty(_priceHistoryVM.TickerSymbol))
                        _priceHistoryVM[e.PropertyName] = _messages[e.PropertyName]["Required"];
                    else
                        _priceHistoryVM[e.PropertyName] = string.Empty;
                    break;
            }
        }
    }
}
