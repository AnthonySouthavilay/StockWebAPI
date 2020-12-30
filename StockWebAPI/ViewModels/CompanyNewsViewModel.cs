using StockWebAPI.Models.Finnhub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebAPI.ViewModels
{
    public class CompanyNewsViewModel
    {
        public DateTime Date { get; set; }
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

        private static DateTime UnixTimestampToDateTime(double unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            String shortDateTime = new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc).ToShortDateString();
            return Convert.ToDateTime(shortDateTime);
        }
    }
}
