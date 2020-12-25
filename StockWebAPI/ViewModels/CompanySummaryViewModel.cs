using StockWebAPI.Models.IEXCloud;
using System;

namespace StockWebAPI.ViewModels
{
    public class CompanySummaryViewModel
    {
        public string CompanyName { get; set; }
        public string Symbol { get; set; }
        public string Exchange { get; set; }
        public double PreviousClose { get; set; }
        public double OpenPrice { get; set; }
        public double Price { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public double HighPrice { get; set; }
        public double LowPrice { get; set; }
        /// <summary>
        /// Price between Price and PreviousClose
        /// </summary>
        public double PriceChange { get; set; }
        /// <summary>
        /// Percentage between Price and PreviousClose
        /// </summary>
        public double PriceChangePercent { get; set; }
        public double Week52High { get; set; }
        public double Week52Low { get; set; }
        public int Volume { get; set; }
        public int AverageVolume { get; set; }
        public long MarketCap { get; set; }
        public double PERatio { get; set; }
        public double EPS { get; set; }
        public double DividendYield { get; set; }

        public CompanySummaryViewModel ConvertToCompanySummaryViewModel(Quote quote)
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
                Week52High = quote.week52High,
                Week52Low = quote.week52Low,
                Volume = quote.volume,
                AverageVolume = quote.avgTotalVolume,
                MarketCap = quote.marketCap,
                PERatio = quote.peRatio
            };
        }
    }
}
