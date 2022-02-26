using Keesh.Interface.User.ViewBehavior;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Keesh.Interface.User.ViewModel
{
    public class PortfolioItemVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly List<object> _behaviors = new List<object>();
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private string _symbol;
        private decimal? _price;
        private decimal? _shares;
        private decimal? _principal;
        private decimal? _equity;
        private decimal? _recurringInvestment;
        private decimal? _meanAnnualReturn;
        private decimal? _equityRatio;
        private decimal? _targetEquityRatio;
        private decimal? _rebalance;
        private Brush _foregroundColor = Brushes.Black;

        public event PropertyChangedEventHandler PropertyChanged;

        public PortfolioItemVM()
        {
            _behaviors.Add(new PortfolioItemBehavior(this));
        }

        public Brush ForegroundColor
        {
            get => _foregroundColor;
            set
            {
                _foregroundColor = value;
                NotifyPropertyChanged();
            }
        }

        public string Symbol 
        { 
            get => _symbol; 
            set
            {
                if (_symbol != value)
                {
                    _symbol = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? Shares
        {
            get => _shares;
            set
            {
                if (_shares != value)
                {
                    _shares = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? Principal
        {
            get => _principal;
            set
            {
                if (_principal != value)
                {
                    _principal = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? Equity
        {
            get => _equity;
            set
            {
                if (_equity != value)
                {
                    _equity = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? RecurringInvestment
        {
            get => _recurringInvestment;
            set
            {
                if (_recurringInvestment != value)
                {
                    _recurringInvestment = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? MeanAnnualReturn
        {
            get => _meanAnnualReturn;
            set
            {
                if (_meanAnnualReturn != value)
                {
                    _meanAnnualReturn = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? EquityRatio
        {
            get => _equityRatio;
            set
            {
                if (_equityRatio != value)
                {
                    _equityRatio = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? TargetEquityRatio
        {
            get => _targetEquityRatio;
            set
            {
                if (_targetEquityRatio != value)
                {
                    _targetEquityRatio = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? Rebalance
        {
            get => _rebalance;
            set
            {
                if (_rebalance != value)
                {
                    _rebalance = value;
                    NotifyPropertyChanged();
                }
            }
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

        public void AddBehavior(object behavior)
        {
            _behaviors.Add(behavior);
        }
    }
}
