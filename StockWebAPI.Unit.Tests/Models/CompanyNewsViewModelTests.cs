﻿using FluentAssertions;
using NUnit.Framework;
using StockWebAPI.Models.Finnhub;
using StockWebAPI.Models.GNews;
using StockWebAPI.ViewModels;

namespace StockWebAPI.Unit.Tests.Models
{
    class CompanyNewsViewModelTests
    {
        private readonly CompanyNewsViewModel companyNewsViewModel = new CompanyNewsViewModel();

        [Test]
        public void ConvertToCompanyNewsViewModel_FinnhubCompanyNewsModel_ReturnsCompanyNewsViewModel()
        {
            FinnhubCompanyNews companyNews = new FinnhubCompanyNews()
            {
                image = "https://www.TestImage.com",
                source = "CNNFOX",
                url = "https://www.FakeArticle.com"
            };
            CompanyNewsViewModel result = companyNewsViewModel.ConvertToCompanyNewsViewModel(companyNews);
            result.Should().NotBeNull();
        }

        [Test]
        public void ConvertToCompanyNewsViewModel_FinnhubCompanyNewsModel_ReturnsCorrectDateTime()
        {
            FinnhubCompanyNews companyNews = new FinnhubCompanyNews()
            {
                datetime = 1588333261
            };
            string expectedDateTime = "5/1/2020";
            CompanyNewsViewModel result = companyNewsViewModel.ConvertToCompanyNewsViewModel(companyNews);
            result.Date.Should().Be(expectedDateTime);
        }

        [Test]
        public void ConvertToCompanyNewsViewModel_GNewsCompanyNewsModel_ReturnsCompanyNewsViewModel()
        {
            GNewsCompanyNews.Article companyNewsArticle
                = new GNewsCompanyNews.Article()
            {
                title = "test article",
                url = "www.test.com",
                source = new GNewsCompanyNews.Source()
                {
                    name = "Test News",
                    url = "www.testNewsPaper.com"
                }
            };
            CompanyNewsViewModel result = companyNewsViewModel.ConvertToCompanyNewsViewModel(companyNewsArticle);
            result.Should().NotBeNull();
        }
    }
}
