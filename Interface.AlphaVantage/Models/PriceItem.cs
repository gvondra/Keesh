using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Keesh.Interface.AlphaVantage.Models
{
    public class PriceItem
    {
        [Name("timestamp")]
        public DateTime? Timestamp { get; set; }
        [Name("open")]
        public decimal? Open { get; set; }
        [Name("high")]
        public decimal? High { get; set; }
        [Name("low")]
        public decimal? Low { get; set; }
        [Name("close")]
        public decimal? Close { get; set; }
        [Name("volume")]
        public int? Volume { get; set; }
    }
}
