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
    public class CalendarVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private ApiKeyVM _apiKeyVM;
        private ObservableCollection<Model.EarningsCalendarItem> _earningsCalendarItems = new ObservableCollection<Model.EarningsCalendarItem>();
        private ObservableCollection<Model.IPOCalendarItem> _ipoCalendarItems = new ObservableCollection<Model.IPOCalendarItem>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ApiKeyVM ApiKey
        {
            get => _apiKeyVM;
            set
            {
                _apiKeyVM = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Model.EarningsCalendarItem> EarningsCalendarItems
        {
            get => _earningsCalendarItems;
        }

        public ObservableCollection<Model.IPOCalendarItem> IpoCalendarItems
        {
            get => _ipoCalendarItems;
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
