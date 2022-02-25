using Keesh.Interface.User.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.InputValidator
{
    public class CompanyOverviewValidator
    {
        private const string IS_REQUIRED = " is required";
        private readonly Dictionary<string, Dictionary<string, string>> _messages = new Dictionary<string, Dictionary<string, string>>
        {
            { 
                nameof(CompanyOverviewVM.TickerSymbol), 
                new Dictionary<string, string>
                {
                    { "Required", IS_REQUIRED }
                }
            }
        };
        private readonly CompanyOverviewVM _companyOverviewVM;

        public CompanyOverviewValidator(CompanyOverviewVM companyOverviewVM)
        {
            _companyOverviewVM = companyOverviewVM;
            companyOverviewVM.PropertyChanged += OnProperyChanged;
        }

        public void OnProperyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CompanyOverviewVM.TickerSymbol):
                    if (string.IsNullOrEmpty(_companyOverviewVM.TickerSymbol))
                        _companyOverviewVM[e.PropertyName] = _messages[e.PropertyName]["Required"];
                    else
                        _companyOverviewVM[e.PropertyName] = string.Empty;
                    break;
            }
        }
    }
}
