namespace StockWebAPI.Unit.Tests.Repositories
{
    public class FinnhubRecommendationTrends
    {
        public int Buy { get; set; }
        public int Hold { get; set; }
        public string Period { get; set; }
        public int Sell { get; set; }
        public int StrongBuy { get; set; }
        public int StrongSell { get; set; }
        public string Symbol { get; set; }
    }
}