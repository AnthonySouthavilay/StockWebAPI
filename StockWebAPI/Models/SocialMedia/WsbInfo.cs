namespace StockWebAPI.Models.SocialMedia
{
    public class WsbInfo
    {
        public int No_of_comments { get; set; }
        public string Sentiment { get; set; }
        public double Sentiment_score { get; set; }
        public string Ticker { get; set; }
    }
}