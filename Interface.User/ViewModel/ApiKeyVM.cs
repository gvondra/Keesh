using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.ViewModel
{
    public class ApiKeyVM : INotifyPropertyChanged
    {
        private string _key = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

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

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
