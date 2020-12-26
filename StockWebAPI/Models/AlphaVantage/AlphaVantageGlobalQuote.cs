using Newtonsoft.Json;

namespace StockWebAPI.Models.AlphaVantage
{
    public class AlphaVantageQuote
    {
        private string changePercent;

        [JsonProperty("01. symbol")]
        public string Symbol { get; set; }
        [JsonProperty("02. open")]
        public string Open { get; set; }
        [JsonProperty("03. high")]
        public string High { get; set; }
        [JsonProperty("04. low")]
        public string Low { get; set; }
        [JsonProperty("05. price")]
        public string Price { get; set; }
        [JsonProperty("06. volume")]
        public string Volume { get; set; }
        [JsonProperty("07. latest trading day")]
        public string LatestTradingDay { get; set; }
        [JsonProperty("08. previous close")]
        public string PreviousClose { get; set; }
        [JsonProperty("09. change")]
        public string Change { get; set; }
        [JsonProperty("10. change percent")]
        public string ChangePercent 
        { 
            get { return changePercent; } 
            set { changePercent = value.Substring(0,value.Length - 1); } 
        } 
    }

    public class AlphaVantageGlobalQuote
    {
        [JsonProperty("Global Quote")]
        public AlphaVantageQuote GlobalQuote { get; set; }
    }
}
