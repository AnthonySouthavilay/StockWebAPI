﻿namespace StockWebAPI.Models.IEXCloud
{
    public class Quote
    {
        public string symbol { get; set; }
        public string companyName { get; set; }
        public string primaryExchange { get; set; }
        public string calculationPrice { get; set; }
        public double open { get; set; }
        public long openTime { get; set; }
        public string openSource { get; set; }
        public double close { get; set; }
        public long closeTime { get; set; }
        public string closeSource { get; set; }
        public double high { get; set; }
        public long highTime { get; set; }
        public string highSource { get; set; }
        public double low { get; set; }
        public long lowTime { get; set; }
        public string lowSource { get; set; }
        public double latestPrice { get; set; }
        public string latestSource { get; set; }
        public string latestTime { get; set; }
        public long latestUpdate { get; set; }
        public int latestVolume { get; set; }
        public double iexRealtimePrice { get; set; }
        public int iexRealtimeSize { get; set; }
        public long iexLastUpdated { get; set; }
        public double delayedPrice { get; set; }
        public long delayedPriceTime { get; set; }
        public double oddLotDelayedPrice { get; set; }
        public long oddLotDelayedPriceTime { get; set; }
        public double extendedPrice { get; set; }
        public double extendedChange { get; set; }
        public double extendedChangePercent { get; set; }
        public long extendedPriceTime { get; set; }
        public double previousClose { get; set; }
        public int previousVolume { get; set; }
        public double change { get; set; }
        public double changePercent { get; set; }
        public int volume { get; set; }
        public double iexMarketPercent { get; set; }
        public int iexVolume { get; set; }
        public int avgTotalVolume { get; set; }
        public int iexBidPrice { get; set; }
        public int iexBidSize { get; set; }
        public int iexAskPrice { get; set; }
        public int iexAskSize { get; set; }
        public double iexOpen { get; set; }
        public long iexOpenTime { get; set; }
        public double iexClose { get; set; }
        public long iexCloseTime { get; set; }
        public long marketCap { get; set; }
        public double peRatio { get; set; }
        public double week52High { get; set; }
        public double week52Low { get; set; }
        public double ytdChange { get; set; }
        public long lastTradeTime { get; set; }
        public bool isUSMarketOpen { get; set; }
    }


}