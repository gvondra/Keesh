using Keesh.Interface.User.InputValidator;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.ViewModel
{
    public class PriceHistoryVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly PriceHistoryValidator _validator;
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private ApiKeyVM _apiKeyVM;
        private string _tickerSymbol;
        private ObservableCollection<Model.PriceItem> _priceItems = new ObservableCollection<Model.PriceItem>();

        public event PropertyChangedEventHandler PropertyChanged;

        public PriceHistoryVM()
        {
            _validator = new PriceHistoryValidator(this);
        }

        public ApiKeyVM ApiKey
        {
            get => _apiKeyVM;
            set
            {
                _apiKeyVM = value;
                NotifyPropertyChanged();
            }
        }

        public string TickerSymbol
        {
            get => _tickerSymbol;
            set
            {
                if (value != _tickerSymbol)
                {
                    _tickerSymbol = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<Model.PriceItem> PriceItems
        {
            get => _priceItems;
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get => _errors.ContainsKey(columnName) ? _errors[columnName] : null;
            set => _errors[columnName] = value;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
