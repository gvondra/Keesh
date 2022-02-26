using Keesh.Interface.User.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Keesh.Interface.User.ViewBehavior
{
    public class PortfolioItemBehavior
    {
        private readonly PortfolioItemVM _portfolioItem;

        public PortfolioItemBehavior(PortfolioItemVM portfolioItemVM)
        {
            _portfolioItem = portfolioItemVM;
            portfolioItemVM.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PortfolioItemVM.Price):
                    UpdateEquity();
                    break;
                case nameof(PortfolioItemVM.Shares):
                    UpdateEquity();
                    break;
                case nameof(PortfolioItemVM.Equity):
                case nameof(PortfolioItemVM.Principal):
                    UpdateForegroundColor();
                    break;
            }
        }

        private void UpdateEquity()
        {
            if (_portfolioItem.Price.HasValue && _portfolioItem.Shares.HasValue)
                _portfolioItem.Equity = _portfolioItem.Price.Value * _portfolioItem.Shares.Value;
            else if (!string.IsNullOrEmpty(_portfolioItem.Symbol))
                _portfolioItem.Equity = 0.0M;
            else
                _portfolioItem.Equity = null;
        }

        private void UpdateForegroundColor()
        {
            Brush color = Brushes.Black;
            if (_portfolioItem.Principal.HasValue && _portfolioItem.Equity.HasValue && _portfolioItem.Equity.Value < _portfolioItem.Principal.Value)
                color = Brushes.Red;
            _portfolioItem.ForegroundColor = color;
        }
    }
}
