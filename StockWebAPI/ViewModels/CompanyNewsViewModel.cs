using StockWebAPI.Models.Finnhub;
using StockWebAPI.Models.GNews;
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
        public CompanyNewsViewModel ConvertToCompanyNewsViewModel(FinnhubCompanyNews companyNews)
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
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            string shortDateTime = new DateTime(unixStart.Ticks + unixTimeStampInTicks, DateTimeKind.Utc).ToShortDateString();
            return shortDateTime;
        }

        public CompanyNewsViewModel ConvertToCompanyNewsViewModel(GNewsCompanyNews.Article article)
        {
            CompanyNewsViewModel companyNewsViewModel = new CompanyNewsViewModel()
            {
                Date = article.publishedAt.ToShortDateString(),
                Headline = article.title,
                ImageUrl = article.image,
                Source = article.source.name,
                Summary = article.description,
                ArticleUrl = article.url
            };
            return companyNewsViewModel;
        }
    }
}
