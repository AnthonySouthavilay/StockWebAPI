using FluentAssertions;
using NUnit.Framework;
using StockWebAPI.Models.Finnhub;
using StockWebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWebAPI.Unit.Tests.Models
{
    class CompanyNewsViewModelTests
    {
        private CompanyNewsViewModel companyNewsViewModel = new CompanyNewsViewModel();

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
    }
}
