using FluentAssertions;
using NUnit.Framework;
using StockWebAPI.Models.Finnhub;
using StockWebAPI.ViewModels;
using System;

namespace StockWebAPI.Unit.Tests.Models
{
    class CompanyNewsViewModelTests
    {
        private readonly CompanyNewsViewModel companyNewsViewModel = new CompanyNewsViewModel();

        [Test]
        public void ConvertToCompanyNewsViewModel_CompanyNewsModel_ReturnsCompanyNewsViewModel()
        {
            CompanyNews companyNews = new CompanyNews()
            {
                image = "https://www.TestImage.com",
                source = "CNNFOX",
                url = "https://www.FakeArticle.com"
            };
            CompanyNewsViewModel result = companyNewsViewModel.ConvertToCompanyNewsViewModel(companyNews);
            result.Should().NotBeNull();
        }

        [Test]
        public void ConvertToCompanyNewsViewModel_CompanyNewsModel_ReturnsCorrectDateTime()
        {
            CompanyNews companyNews = new CompanyNews()
            {
                datetime = 1588333261
            };
            DateTime expectedDateTime = new DateTime(2020, 5, 1);
            CompanyNewsViewModel result = companyNewsViewModel.ConvertToCompanyNewsViewModel(companyNews);
            result.Date.Should().Be(expectedDateTime);
        }
    }
}
