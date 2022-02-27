using Keesh.Interface.User.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Keesh.Interface.User.ViewBehavior
{
    public class PortfolioBehavior
    {
        private readonly PortfolioVM _portfolio;

        public PortfolioBehavior(PortfolioVM portfolio)
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
                case nameof(PortfolioItemVM.Principal):
                    UpdateTotalPrincipal();
                    break;
                case nameof(PortfolioItemVM.Equity):
                    UpdateTotalEquity();
                    break;
                case nameof(PortfolioItemVM.RecurringInvestment):
                    UpdateTotalRecurringInvestment();
                    break;
                case nameof(PortfolioItemVM.EquityRatio):
                    UpdateTotalEquityRatio();
                    break;
                case nameof(PortfolioItemVM.TargetEquityRatio):
                    UpdateTotalTargetEquityRatio();
                    break;
                case nameof(PortfolioItemVM.Rebalance):
                    UpdateTotalRebalance();
                    break;
            }
        }

        private void UpdateTotalPrincipal()
        {
            _portfolio.TotalPrincipal = _portfolio.Items
                .Where(i => i.Principal.HasValue)
                .Select<PortfolioItemVM, decimal>(i => i.Principal.Value)
                .Sum();
            UpdateTotalEquityColor();
        }

        private void UpdateTotalEquity()
        {
            _portfolio.TotalEquity = _portfolio.Items
                .Where(i => i.Equity.HasValue)
                .Select<PortfolioItemVM, decimal>(i => i.Equity.Value)
                .Sum();
            UpdateTotalEquityColor();
        }

        private void UpdateTotalEquityColor()
        {
            if (_portfolio.TotalEquity < _portfolio.TotalPrincipal)
                _portfolio.TotalEquityColor = Brushes.Red;
            else
                _portfolio.TotalEquityColor = Brushes.Black;
        }

        private void UpdateTotalRecurringInvestment()
        {
            _portfolio.TotalRecurringInvestment = _portfolio.Items
                .Where(i => i.RecurringInvestment.HasValue)
                .Select<PortfolioItemVM, decimal>(i => i.RecurringInvestment.Value)
                .Sum();
        }

        private void UpdateTotalEquityRatio()
        {
            _portfolio.TotalEquityRatio = _portfolio.Items
                .Where(i => i.EquityRatio.HasValue)
                .Select<PortfolioItemVM, decimal>(i => i.EquityRatio.Value)
                .Sum();
        }

        private void UpdateTotalTargetEquityRatio()
        {
            _portfolio.TotalTargetEquityRatio = _portfolio.Items
                .Where(i => i.TargetEquityRatio.HasValue)
                .Select<PortfolioItemVM, decimal>(i => i.TargetEquityRatio.Value)
                .Sum();
        }

        private void UpdateTotalRebalance()
        {
            _portfolio.TotalRebalance = _portfolio.Items
                .Where(i => i.Rebalance.HasValue)
                .Select<PortfolioItemVM, decimal>(i => i.Rebalance.Value)
                .Sum();
        }
    }
}
