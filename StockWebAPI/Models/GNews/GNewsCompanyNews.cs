using System;
using System.Collections.Generic;

namespace StockWebAPI.Models.GNews
{
    public class GNewsCompanyNews
    {
        public class Source
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }

        public class Article
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Content { get; set; }
            public string Url { get; set; }
            public string Image { get; set; }
            public DateTime PublishedAt { get; set; }
            public Source Source { get; set; }
        }

        public class GNewsModel
        {
            public int TotalArticles { get; set; }
            public List<Article> Articles { get; set; }
        }
    }
}
