using Keesh.Interface.User.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.ViewBehavior
{
    public class PortfolioScheduleCalculator
    {
        private readonly PortfolioVM _portfolio;

        public PortfolioScheduleCalculator(PortfolioVM portfolio)
        {
            _portfolio = portfolio;
            _portfolio.Items.CollectionChanged += Items_CollectionChanged;
            portfolio.PropertyChanged += Portfolio_PropertyChanged;
        }

        private void Portfolio_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PortfolioVM.StartMonth):
                case nameof(PortfolioVM.SheduleTerm):
                    Calculate();
                    break;
            }
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
                case nameof(PortfolioItemVM.MeanAnnualReturn):
                    Calculate();
                    break;
            }
        }

        private void Calculate()
        {
            DataTable table = _portfolio.Schedule;
            if (table == null)
                table = new DataTable();
            PortfolioItemVM[] portfolioItems = GetPortFolioItems().ToArray();
            InitializeColumns(table);
            InitializeHeader(table, portfolioItems);
            InitalizeRows(table);
            CalculateSchedule(table, portfolioItems);
            if (_portfolio.Schedule == null)
                _portfolio.Schedule = table;
        }

        private void CalculateSchedule(DataTable table, PortfolioItemVM[] portfolioItems)
        {
            const int columnOffset = 1;
            const int rowStart = 1;
            decimal previousTermValue;
            decimal termTotal;
            decimal termValue;
            int termTotalIndex = TotalColumnCount() - 1;
            int term = Math.Abs(_portfolio.StartMonth);
            // start j at 1 to skip the header
            for (int j = rowStart; j < table.Rows.Count; j += 1)
            {
                table.Rows[j][0] = term;
                termTotal = 0.0M;
                for (int i = 0; i < portfolioItems.Length; i += 1)
                {
                    if (j == rowStart)
                        termValue = Math.Round(portfolioItems[i].Equity.HasValue ? portfolioItems[i].Equity.Value : 0.0M, 2, MidpointRounding.ToEven);
                    else
                    {
                        previousTermValue = decimal.Parse((string)table.Rows[j - 1][i + columnOffset]);
                        termValue = GetScheduleValue(portfolioItems[i], previousTermValue);
                    }
                    table.Rows[j][i + columnOffset] = termValue;
                    termTotal += termValue;
                }
                table.Rows[j][termTotalIndex] = termTotal;
                term += 1;
            }
        }        

        private decimal GetScheduleValue(PortfolioItemVM portfolioItem, decimal previousTermValue)
        {
            decimal rate = 1.0M + Math.Round((portfolioItem.MeanAnnualReturn.HasValue ? portfolioItem.MeanAnnualReturn.Value : 0.0M) / 12.0M, 4, MidpointRounding.ToEven);
            return Math.Round(previousTermValue * rate, 2, MidpointRounding.ToEven)
                + (portfolioItem.RecurringInvestment.HasValue ? portfolioItem.RecurringInvestment.Value : 0.0M);
        }

        private void InitalizeRows(DataTable table)
        {
            int j = table.Rows.Count - 1;
            while (table.Rows.Count > _portfolio.SheduleTerm + 1)
            {
                table.Rows.RemoveAt(j);
                j -= 1;
            }
            for (j = table.Rows.Count; j <= _portfolio.SheduleTerm; j += 1)
            {
                table.Rows.Add(table.NewRow());
            }
        }

        private void InitializeHeader(DataTable table, PortfolioItemVM[] portfolioItems)
        {
            if (table.Rows.Count == 0)
                table.Rows.Add(table.NewRow());
            table.Rows[0][0] = "Month";
            for (int i = 0; i < portfolioItems.Length; i += 1)
            {
                table.Rows[0][i + 1] = portfolioItems[i].Symbol;
            }
            table.Rows[0][TotalColumnCount() - 1] = "Total";
        }

        private void InitializeColumns(DataTable table)
        {
            int i = table.Columns.Count - 1;
            while (table.Columns.Count > TotalColumnCount())
            {
                table.Columns.RemoveAt(i);
                i -= 1;
            }
            for (i = table.Columns.Count; i < TotalColumnCount(); i+=1)
            {
                table.Columns.Add(GetColumnName(i), typeof(string));
            }
        }

        private int TotalColumnCount() => GetPortFolioItems().Count() + 2; // +2 for term column and total column

        private string GetColumnName(int index) => $"col{index}";

        private IEnumerable<PortfolioItemVM> GetPortFolioItems()
            => _portfolio.Items.Where(i => (i.Equity.HasValue && i.Equity.Value > 0.0M) || (i.RecurringInvestment.HasValue && i.RecurringInvestment.Value > 0.0M));
    }
}
