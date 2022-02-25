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
    public class CompanyOverviewVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly CompanyOverviewValidator _validator;
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private ApiKeyVM _apiKeyVM;
        private string _tickerSymbol;
        private string _assetType;
        private string _name;
        private string _description;
        private long? _cik;
        private string _exchange;
        private string _currency;
        private string _country;
        private string _sector;
        private string _industry;
        private string _address;
        private string _fiscalYearEnd;
        private DateTime? _latestQuarter;
        private decimal? _marketCapitalization;
        private decimal? _ebitda;
        private decimal? _peRatio;
        private decimal? _pegRatio;
        private decimal? _bookValue;
        private decimal? _dividendPerShare;
        private decimal? _dividendYield;
        private decimal? _eps;
        private decimal? _revenuePerShareTTM;
        private decimal? _profitMargin;
        private decimal? _operatingMarginTTM;
        private decimal? _returnOnAssetsTTM;
        private decimal? _returnOnEquityTTM;
        private decimal? _revenueTTM;
        private decimal? _grossProfitTTM;
        private decimal? _dilutedEPSTTM;
        private decimal? _quarterlyEarningsGrowthYOY;
        private decimal? _quarterlyRevenueGrowthYOY;
        private decimal? _analystTargetPrice;
        private decimal? _trailingPE;
        private decimal? _forwardPE;
        private decimal? _priceToSalesRatioTTM;
        private string _priceToBookRatio;
        private decimal? _evToRevenue;
        private decimal? _evToEBITDA;
        private decimal? _beta;
        private decimal? _high52Week;
        private decimal? _low52Week;
        private decimal? _movingAverage50Day;
        private decimal? _movingAverage200Day;
        private long? _sharesOutstanding;
        private string _dividendDate;
        private string _exDividendDate;

        public event PropertyChangedEventHandler PropertyChanged;

        public CompanyOverviewVM()
        {
            _validator = new CompanyOverviewValidator(this);
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

        public string AssetType
        {
            get => _assetType;
            set
            {
                if (value != _assetType)
                {
                    _assetType = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public long? CIK
        {
            get => _cik;
            set
            {
                if (value != _cik)
                {
                    _cik = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Exchange
        {
            get => _exchange;
            set
            {
                if (value != _exchange)
                {
                    _exchange = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Currency
        {
            get => _currency;
            set
            {
                if (value != _currency)
                {
                    _currency = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Sector
        {
            get => _sector;
            set
            {
                if (value != _sector)
                {
                    _sector = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Industry
        {
            get => _industry;
            set
            {
                if (value != _industry)
                {
                    _industry = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                if (value != _address)
                {
                    _address = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FiscalYearEnd
        {
            get => _fiscalYearEnd;
            set
            {
                if (value != _fiscalYearEnd)
                {
                    _fiscalYearEnd = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DateTime? LatestQuarter
        {
            get => _latestQuarter;
            set
            {
                if (value != _latestQuarter)
                {
                    _latestQuarter = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? MarketCapitalization
        {
            get => _marketCapitalization;
            set
            {
                if (value != _marketCapitalization)
                {
                    _marketCapitalization = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? EBITDA
        {
            get => _ebitda;
            set
            {
                if (value != _ebitda)
                {
                    _ebitda = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? PERatio
        {
            get => _peRatio;
            set
            {
                if (value != _peRatio)
                {
                    _peRatio = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? PEGRatio
        {
            get => _pegRatio;
            set
            {
                if (value != _pegRatio)
                {
                    _pegRatio = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? BookValue
        {
            get => _bookValue;
            set
            {
                if (value != _bookValue)
                {
                    _bookValue = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? DividendPerShare
        {
            get => _dividendPerShare;
            set
            {
                if (value != _dividendPerShare)
                {
                    _dividendPerShare = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? DividendYield
        {
            get => _dividendYield;
            set
            {
                if (value != _dividendYield)
                {
                    _dividendYield = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? EPS
        {
            get => _eps;
            set
            {
                if (value != _eps)
                {
                    _eps = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? RevenuePerShareTTM
        {
            get => _revenuePerShareTTM;
            set
            {
                if (value != _revenuePerShareTTM)
                {
                    _revenuePerShareTTM = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? ProfitMargin
        {
            get => _profitMargin;
            set
            {
                if (value != _profitMargin)
                {
                    _profitMargin = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? OperatingMarginTTM
        {
            get => _operatingMarginTTM;
            set
            {
                if (value != _operatingMarginTTM)
                {
                    _operatingMarginTTM = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? ReturnOnAssetsTTM
        {
            get => _returnOnAssetsTTM;
            set
            {
                if (value != _returnOnAssetsTTM)
                {
                    _returnOnAssetsTTM = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? ReturnOnEquityTTM
        {
            get => _returnOnEquityTTM;
            set
            {
                if (value != _returnOnEquityTTM)
                {
                    _returnOnEquityTTM = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? RevenueTTM
        {
            get => _revenueTTM;
            set
            {
                if (value != _revenueTTM)
                {
                    _revenueTTM = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? GrossProfitTTM
        {
            get => _grossProfitTTM;
            set
            {
                if (value != _grossProfitTTM)
                {
                    _grossProfitTTM = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? DilutedEPSTTM
        {
            get => _dilutedEPSTTM;
            set
            {
                if (value != _dilutedEPSTTM)
                {
                    _dilutedEPSTTM = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? QuarterlyEarningsGrowthYOY
        {
            get => _quarterlyEarningsGrowthYOY;
            set
            {
                if (value != _quarterlyEarningsGrowthYOY)
                {
                    _quarterlyEarningsGrowthYOY = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? QuarterlyRevenueGrowthYOY
        {
            get => _quarterlyRevenueGrowthYOY;
            set
            {
                if (value != _quarterlyRevenueGrowthYOY)
                {
                    _quarterlyRevenueGrowthYOY = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? AnalystTargetPrice
        {
            get => _analystTargetPrice;
            set
            {
                if (value != _analystTargetPrice)
                {
                    _analystTargetPrice = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? TrailingPE
        {
            get => _trailingPE;
            set
            {
                if (value != _trailingPE)
                {
                    _trailingPE = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? ForwardPE
        {
            get => _forwardPE;
            set
            {
                if (value != _forwardPE)
                {
                    _forwardPE = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? PriceToSalesRatioTTM
        {
            get => _priceToSalesRatioTTM;
            set
            {
                if (value != _priceToSalesRatioTTM)
                {
                    _priceToSalesRatioTTM = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string PriceToBookRatio
        {
            get => _priceToBookRatio;
            set
            {
                if (value != _priceToBookRatio)
                {
                    _priceToBookRatio = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? EVToRevenue
        {
            get => _evToRevenue;
            set
            {
                if (value != _evToRevenue)
                {
                    _evToRevenue = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? EVToEBITDA
        {
            get => _evToEBITDA;
            set
            {
                if (value != _evToEBITDA)
                {
                    _evToEBITDA = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? Beta
        {
            get => _beta;
            set
            {
                if (value != _beta)
                {
                    _beta = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? High52Week
        {
            get => _high52Week;
            set
            {
                if (value != _high52Week)
                {
                    _high52Week = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? Low52Week
        {
            get => _low52Week;
            set
            {
                if (value != _low52Week)
                {
                    _low52Week = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? MovingAverage50Day
        {
            get => _movingAverage50Day;
            set
            {
                if (value != _movingAverage50Day)
                {
                    _movingAverage50Day = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? MovingAverage200Day
        {
            get => _movingAverage200Day;
            set
            {
                if (value != _movingAverage200Day)
                {
                    _movingAverage200Day = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public long? SharesOutstanding
        {
            get => _sharesOutstanding;
            set
            {
                if (value != _sharesOutstanding)
                {
                    _sharesOutstanding = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string DividendDate
        {
            get => _dividendDate;
            set
            {
                if (value != _dividendDate)
                {
                    _dividendDate = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string ExDividendDate
        {
            get => _exDividendDate;
            set
            {
                if (value != _exDividendDate)
                {
                    _exDividendDate = value;
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
    }
}
