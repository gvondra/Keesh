using System;
using System.Collections.Generic;
using System.Text;

namespace Keesh.Interface.User.Model
{
    public class EarningsCalendarItem
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public DateTime? ReportDate { get; set; }
        public DateTime? FiscalDateEnding { get; set; }
        public string Estimate { get; set; }
        public string Currency { get; set; }
    }
}
