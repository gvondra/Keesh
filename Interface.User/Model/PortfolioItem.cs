using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Model
{
    public class PortfolioItem
    {
        public string Symbol { get; set; }
        public decimal? Price { get; set; }
        public decimal? Shares { get; set; }
        public decimal? Principal { get; set; }
        public decimal? Equity { get; set; }
        public decimal? RecurringInvestment { get; set; }
        public decimal? MainAnnualReturn { get; set; }
        public decimal? EquityRatio { get; set; }
        public decimal? TargetEquityRatio { get; set; }
        public decimal? Rebalance { get; set; }
    }
}
