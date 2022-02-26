using Keesh.Interface.User.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.ViewBehavior
{
    public class PortfolioRebalanceCalculator
    {
        private readonly PortfolioVM _portfolio;

        public PortfolioRebalanceCalculator(PortfolioVM portfolio)
        {
            _portfolio = portfolio;
            _portfolio.Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (object item in e.NewItems)
                {
                    ((PortfolioItemVM)item).PropertyChanged += PortfolioItem_PropertyChanged;
                }
        }

        private void PortfolioItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PortfolioItemVM.Equity):
                case nameof(PortfolioItemVM.TargetEquityRatio):
                    UpdateEquityRatio();
                    break;
            }
        }

        private void UpdateEquityRatio()
        {
            decimal totalEquity = _portfolio.Items
                .Where(i => i.Equity.HasValue)
                .Select<PortfolioItemVM, decimal>(i => i.Equity.Value)
                .Sum();
            foreach (PortfolioItemVM item in _portfolio.Items.Where(i => i.Equity.HasValue && i.TargetEquityRatio.HasValue))
            {
                item.Rebalance = Math.Round((totalEquity * item.TargetEquityRatio.Value) - item.Equity.Value, 3, MidpointRounding.ToEven);
            }
        }
    }
}
