using Keesh.Interface.User.InputValidator;
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
    public class ApiKeyVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly ApiKeyValidator _apiKeyValidator;
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private string _key = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public ApiKeyVM()
        {
            _apiKeyValidator = new ApiKeyValidator(this);
        }

        public string Key 
        { 
            get => (_key ?? string.Empty).Trim(); 
            set
            {
                value = (value ?? string.Empty).Trim();
                if (_key != value)
                {
                    _key = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsValid => _errors.Where(e => !string.IsNullOrEmpty(e.Value)).Count() == 0;

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
