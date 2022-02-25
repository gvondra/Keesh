using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Keesh.Interface.AlphaVantage.Models
{
    public class IPOCalendarItem
    {
        [Name("symbol")]
        public string Symbol { get; set; }
        [Name("name")]
        public string Name { get; set; }
        [Name("ipoDate")]
        public DateTime? Date { get; set; }
        [Name("priceRangeLow")]
        public decimal? PriceRangeLow { get; set; }
        [Name("priceRangeHigh")]
        public decimal? PriceRangeHigh { get; set; }
        [Name("currency")]
        public string Currency { get; set; }
        [Name("exchange")]
        public string Exchange { get; set; }
    }
}
