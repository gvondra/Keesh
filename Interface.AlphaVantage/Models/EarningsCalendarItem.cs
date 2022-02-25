using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Keesh.Interface.AlphaVantage.Models
{
    public class EarningsCalendarItem
    {
        [Name("symbol")]
        public string Symbol { get; set; }
        [Name("name")]
        public string Name { get; set; }
        [Name("reportDate")]
        public DateTime? ReportDate { get; set; }
        [Name("fiscalDateEnding")]
        public DateTime? FiscalDateEnding { get; set; }
        [Name("estimate")]
        public string Estimate { get; set; }
        [Name("currency")]
        public string Currency { get; set; }
    }
}
