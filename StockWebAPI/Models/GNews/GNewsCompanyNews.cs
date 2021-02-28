using System;
using System.Collections.Generic;

namespace StockWebAPI.Models.GNews
{
    public class GNewsCompanyNews
    {
        public class Source
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Article
        {
            public string title { get; set; }
            public string description { get; set; }
            public string content { get; set; }
            public string url { get; set; }
            public string image { get; set; }
            public DateTime publishedAt { get; set; }
            public Source source { get; set; }
        }

        public class GNewsModel
        {
            public int totalArticles { get; set; }
            public List<Article> articles { get; set; }
        }
    }
}
