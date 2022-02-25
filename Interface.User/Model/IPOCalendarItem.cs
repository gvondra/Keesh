using System;
using System.Collections.Generic;
using System.Text;

namespace Keesh.Interface.User.Model
{
    public class IPOCalendarItem
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public decimal? PriceRangeLow { get; set; }
        public decimal? PriceRangeHigh { get; set; }
        public string Currency { get; set; }
        public string Exchange { get; set; }
    }
}
