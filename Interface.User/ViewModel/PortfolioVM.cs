using Keesh.Interface.User.ViewBehavior;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Keesh.Interface.User.ViewModel
{
    public class PortfolioVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly List<object> _behaviors = new List<object>();
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private readonly ObservableCollection<PortfolioItemVM> _items = new ObservableCollection<PortfolioItemVM>();
        private ApiKeyVM _apiKeyVM;
        private DataTable _schedule;
        private int _scheduleTerm = 12;
        private decimal _totalPrincipal = 0.0M;
        private decimal _totalEquity = 0.0M;
        private decimal _totalRecurringInvestment = 0.0M;
        private decimal _totalEquityRatio = 0.0M;
        private decimal _totalTargetEquityRatio = 0.0M;
        private decimal _totalRebalance = 0.0M;
        private Brush _totalEquityColor = Brushes.Black;
        private int _startMonth;
        private string _fileName;
        private string _filePath;

        public event PropertyChangedEventHandler PropertyChanged;

        public PortfolioVM()
        {
            _startMonth = (DateTime.Now.Month + 11) % 12;
            _behaviors.Add(new PortfolioBehavior(this));
            _behaviors.Add(new PortfolioEquityRatioCalculator(this));
            _behaviors.Add(new PortfolioRebalanceCalculator(this));
            _behaviors.Add(new PortfolioScheduleCalculator(this));
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

        public DataTable Schedule
        {
            get => _schedule;
            set
            {
                _schedule = value;
                NotifyPropertyChanged();
            }
        }

        public string FileName
        {
            get => _fileName;
            set
            {
                if (_fileName != value)
                {
                    _fileName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string FilePath
        {
            get => _filePath;
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public decimal ProfitLoss
        {
            get => TotalEquity - TotalPrincipal;
        }
        
        public decimal TotalPrincipal
        {
            get => _totalPrincipal;
            set
            {
                if (_totalPrincipal != value)
                {
                    _totalPrincipal = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(ProfitLoss));
                }
            }
        }

        public Brush TotalEquityColor
        {
            get => _totalEquityColor;
            set
            {
                _totalEquityColor = value;
                NotifyPropertyChanged();
            }
        }

        public decimal TotalEquity
        {
            get => _totalEquity;
            set
            {
                if (_totalEquity != value)
                {
                    _totalEquity = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(ProfitLoss));
                }
            }
        }

        public decimal TotalRecurringInvestment
        {
            get => _totalRecurringInvestment;
            set
            {
                if (_totalRecurringInvestment != value)
                {
                    _totalRecurringInvestment = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public decimal TotalEquityRatio
        {
            get => _totalEquityRatio;
            set
            {
                if (_totalEquityRatio != value)
                {
                    _totalEquityRatio = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public decimal TotalTargetEquityRatio
        {
            get => _totalTargetEquityRatio;
            set
            {
                if (_totalTargetEquityRatio != value)
                {
                    _totalTargetEquityRatio = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public decimal TotalRebalance
        {
            get => _totalRebalance;
            set
            {
                if (_totalRebalance != value)
                {
                    _totalRebalance = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int StartMonth
        {
            get => _startMonth;
            set
            {
                if (_startMonth != value)
                {
                    _startMonth = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int SheduleTerm
        {
            get => _scheduleTerm;
            set
            {
                if (value >= 0 && _scheduleTerm != value)
                {
                    _scheduleTerm = value;
                    NotifyPropertyChanged();
                }
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
