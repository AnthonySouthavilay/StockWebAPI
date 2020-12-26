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

        public CompanySummaryViewModel ConvertToCompanySummaryViewModel(IEXQuote quote)
        {
            return new CompanySummaryViewModel()
            {
                CompanyName = quote.companyName,
                Symbol = quote.symbol,
                Exchange = quote.primaryExchange,
                PreviousClose = quote.previousClose,
                OpenPrice = quote.open,
                Price = quote.latestPrice,
                Bid = quote.iexBidPrice,
                Ask = quote.iexAskPrice,
                HighPrice = quote.high,
                LowPrice = quote.low,
                PriceChange = quote.change,
                PriceChangePercent = quote.changePercent,
                Week52High = quote.week52High.ToString(),
                Week52Low = quote.week52Low.ToString(),
                Volume = quote.volume,
                AverageVolume = quote.avgTotalVolume,
                MarketCap = quote.marketCap.ToString(),
                PERatio = quote.peRatio.ToString()
            };
        }
        public CompanySummaryViewModel ConvertToCompanySummaryViewModel(CompanyKeyStats keyStats, AlphaVantageQuote alphaVantageQuote)
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
                OpenPrice = double.Parse(alphaVantageQuote.GlobalQuote.Open),
                Price = double.Parse(alphaVantageQuote.GlobalQuote.Price),
                HighPrice = double.Parse(alphaVantageQuote.GlobalQuote.High),
                LowPrice = double.Parse(alphaVantageQuote.GlobalQuote.Low),
                Volume = int.Parse(alphaVantageQuote.GlobalQuote.Volume),
                PreviousClose = double.Parse(alphaVantageQuote.GlobalQuote.PreviousClose),
                PriceChange = double.Parse(alphaVantageQuote.GlobalQuote.Change),
                PriceChangePercent = double.Parse(alphaVantageQuote.GlobalQuote.ChangePercent),
            };
        }
    }
}
