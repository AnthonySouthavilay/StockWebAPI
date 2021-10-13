namespace StockWebAPI.Models.Finnhub
{
    public class FinnhubCompanyNews
    {
        public string Category { get; set; }
        public int Datetime { get; set; }
        public string Headline { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public string Related { get; set; }
        public string Source { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
    }
}
