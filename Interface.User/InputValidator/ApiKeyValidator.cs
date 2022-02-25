using Keesh.Interface.User.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.InputValidator
{
    public class ApiKeyValidator 
    {
        private const string IS_REQUIRED = " is required";
        private readonly Dictionary<string, Dictionary<string, string>> _messages = new Dictionary<string, Dictionary<string, string>>
        {
            {
                nameof(ApiKeyVM.Key),
                new Dictionary<string, string>
                {
                    { "Required", IS_REQUIRED }
                }
            }
        };
        private readonly ApiKeyVM _apiKeyVM;

        public ApiKeyValidator(ApiKeyVM apiKeyVM)
        {
            _apiKeyVM = apiKeyVM;
            _apiKeyVM.PropertyChanged += OnProperyChanged;
        }

        public void OnProperyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ApiKeyVM.Key):
                    if (string.IsNullOrEmpty(_apiKeyVM.Key))
                        _apiKeyVM[e.PropertyName] = _messages[e.PropertyName]["Required"];
                    else
                        _apiKeyVM[e.PropertyName] = string.Empty;
                    break;
            }
        }
    }
}
