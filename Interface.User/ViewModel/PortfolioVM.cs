using Keesh.Interface.User.ViewBehavior;
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
    public class PortfolioVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly List<object> _behaviors = new List<object>();
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private readonly ObservableCollection<PortfolioItemVM> _items = new ObservableCollection<PortfolioItemVM>();
        private ApiKeyVM _apiKeyVM;

        public event PropertyChangedEventHandler PropertyChanged;

        public PortfolioVM()
        {
            _behaviors.Add(new PortfolioEquityRatioCalculator(this));
            _behaviors.Add(new PortfolioRebalanceCalculator(this));
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

        public ObservableCollection<PortfolioItemVM> Items => _items;

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
