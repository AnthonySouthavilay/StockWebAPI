using StockWebAPI.Models.AlphaVantage;
using StockWebAPI.Models.IEXCloud;
using System;

namespace StockWebAPI.ViewModels
{
    public class CompanySummaryViewModel
    {
        public string CompanyName { get; set; }
        public string Symbol { get; set; }
        public string Exchange { get; set; }
        public double? PreviousClose { get; set; }
        public double? OpenPrice { get; set; }
        public double? Price { get; set; }
        public double? Bid { get; set; }
        public double? Ask { get; set; }
        public double? HighPrice { get; set; }
        public double? LowPrice { get; set; }
        /// <summary>
        /// Price between Price and PreviousClose
        /// </summary>
        public double? PriceChange { get; set; }
        /// <summary>
        /// Percentage between Price and PreviousClose
        /// </summary>
        public double? PriceChangePercent { get; set; }
        public string Week52High { get; set; }
        public string Week52Low { get; set; }
        public int? Volume { get; set; }
        public int? AverageVolume { get; set; }
        public string MarketCap { get; set; }
        public string PERatio { get; set; }
        public string EPS { get; set; }
        public double? DividendYield { get; set; }
        public CompanySummaryViewModel ConvertToCompanySummaryViewModel(IexQuote quote)
        {
            return new CompanySummaryViewModel()
            {
                CompanyName = quote.CompanyName,
                Symbol = quote.Symbol,
                Exchange = quote.PrimaryExchange,
                PreviousClose = quote.PreviousClose,
                OpenPrice = quote.Open,
                Price = quote.LatestPrice,
                Bid = quote.IexBidPrice,
                Ask = quote.IexAskPrice,
                HighPrice = quote.High,
                LowPrice = quote.Low,
                PriceChange = quote.Change,
                PriceChangePercent = quote.ChangePercent,
                Week52High = quote.Week52High.ToString(),
                Week52Low = quote.Week52Low.ToString(),
                Volume = quote.Volume,
                AverageVolume = quote.AvgTotalVolume,
                MarketCap = quote.MarketCap.ToString(),
                PERatio = quote.PeRatio.ToString()
            };
        }
        public CompanySummaryViewModel ConvertToCompanySummaryViewModel(AlphaVantageCompanyKeyStats keyStats, AlphaVantageQuote alphaVantageQuote)
        {
            return new CompanySummaryViewModel()
            {
                CompanyName = keyStats.Name,
                Symbol = keyStats.Symbol,
                Exchange = keyStats.Exchange,
                Week52High = keyStats._52WeekHigh,
                Week52Low = keyStats._52WeekLow,
                MarketCap = keyStats.MarketCapitalization,
                PERatio = keyStats.PERatio,
                EPS = keyStats.EPS,
                OpenPrice = double.Parse(alphaVantageQuote.Open),
                Price = double.Parse(alphaVantageQuote.Price),
                HighPrice = double.Parse(alphaVantageQuote.High),
                LowPrice = double.Parse(alphaVantageQuote.Low),
                Volume = int.Parse(alphaVantageQuote.Volume),
                PreviousClose = double.Parse(alphaVantageQuote.PreviousClose),
                PriceChange = double.Parse(alphaVantageQuote.Change),
                PriceChangePercent = double.Parse(alphaVantageQuote.ChangePercent),
            };
        }
    }
}
