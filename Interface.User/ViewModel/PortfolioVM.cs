using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.ViewModel
{
    public class PortfolioVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private ApiKeyVM _apiKeyVM;

        public event PropertyChangedEventHandler PropertyChanged;

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
