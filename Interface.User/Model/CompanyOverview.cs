﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Keesh.Interface.User.Model
{
    public class CompanyOverview
    {
        public string Symbol { get; set; } 
        public string AssetType { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; } 
        public long? CIK { get; set; } 
        public string Exchange { get; set; } 
        public string Currency { get; set; } 
        public string Country { get; set; } 
        public string Sector { get; set; } 
        public string Industry { get; set; } 
        public string Address { get; set; } 
        public string FiscalYearEnd { get; set; } 
        public DateTime? LatestQuarter { get; set; } 
        public decimal? MarketCapitalization { get; set; } 
        public decimal? EBITDA { get; set; } 
        public string PERatio { get; set; } 
        public decimal? PEGRatio { get; set; } 
        public decimal? BookValue { get; set; } 
        public decimal? DividendPerShare { get; set; } 
        public decimal? DividendYield { get; set; } 
        public decimal? EPS { get; set; } 
        public decimal? RevenuePerShareTTM { get; set; } 
        public decimal? ProfitMargin { get; set; } 
        public decimal? OperatingMarginTTM { get; set; } 
        public decimal? ReturnOnAssetsTTM { get; set; } 
        public decimal? ReturnOnEquityTTM { get; set; } 
        public decimal? RevenueTTM { get; set; } 
        public decimal? GrossProfitTTM { get; set; } 
        public decimal? DilutedEPSTTM { get; set; } 
        public decimal? QuarterlyEarningsGrowthYOY { get; set; } 
        public decimal? QuarterlyRevenueGrowthYOY { get; set; } 
        public decimal? AnalystTargetPrice { get; set; } 
        public string TrailingPE { get; set; } 
        public decimal? ForwardPE { get; set; } 
        public decimal? PriceToSalesRatioTTM { get; set; } 
        public string PriceToBookRatio { get; set; } 
        public decimal? EVToRevenue { get; set; } 
        public decimal? EVToEBITDA { get; set; } 
        public decimal? Beta { get; set; } 
        public decimal? High52Week { get; set; } 
        public decimal? Low52Week { get; set; } 
        public decimal? MovingAverage50Day { get; set; } 
        public decimal? MovingAverage200Day { get; set; } 
        public long SharesOutstanding { get; set; } 
        public string DividendDate { get; set; } 
        public string ExDividendDate { get; set; } 
    }
}
