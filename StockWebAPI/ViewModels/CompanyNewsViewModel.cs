using StockWebAPI.Models.Finnhub;
using System;

namespace StockWebAPI.ViewModels
{
    public class CompanyNewsViewModel
    {
        public string Date { get; set; }
        public string Headline { get; set; }
        public string ImageUrl { get; set; }
        public string Source { get; set; }
        public string Summary { get; set; }
        public string ArticleUrl { get; set; }
        public CompanyNewsViewModel ConvertToCompanyNewsViewModel(CompanyNews companyNews)
        {
            return new CompanyNewsViewModel()
            {
                Date = UnixTimestampToDateTime(companyNews.datetime),
                Headline = companyNews.headline,
                ImageUrl = companyNews.image,
                Source = companyNews.source,
                Summary = companyNews.summary,
                ArticleUrl = companyNews.url
            };
        }

        private static string UnixTimestampToDateTime(double unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            string shortDateTime = new DateTime(unixStart.Ticks + unixTimeStampInTicks, DateTimeKind.Utc).ToShortDateString();
            return shortDateTime;
        }
    }
}
