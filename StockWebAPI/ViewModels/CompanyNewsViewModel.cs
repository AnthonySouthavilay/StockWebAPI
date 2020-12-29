using StockWebAPI.Models.Finnhub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebAPI.ViewModels
{
    public class CompanyNewsViewModel
    {
        public int Date { get; set; }
        public string Headline { get; set; }
        public string ImageUrl { get; set; }
        public string Source { get; set; }
        public string Summary { get; set; }
        public string ArticleUrl { get; set; }
        public CompanyNewsViewModel ConvertToCompanyNewsViewModel(CompanyNews companyNews)
        {
            return new CompanyNewsViewModel()
            {
                Date = companyNews.datetime,
                Headline = companyNews.headline,
                ImageUrl = companyNews.image,
                Source = companyNews.source,
                Summary = companyNews.summary,
                ArticleUrl = companyNews.url
            };
        }
    }
}
