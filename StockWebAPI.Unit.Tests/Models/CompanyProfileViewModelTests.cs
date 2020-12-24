using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using StockWebAPI.Models;
using StockWebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWebAPI.Unit.Tests.Models
{
    class CompanyProfileViewModelTests
    {
        private CompanyProfileViewModel _companyProfileViewModel = new CompanyProfileViewModel();
        
        [Test]
        public void ConvertToCompanyProfileViewModel_CompanyProfile_ReturnsCompanyProfileViewModel()
        {
            CompanyProfile mockCompanyProfile = new CompanyProfile()
            {
                companyName = "Tank Southy LLC",
                symbol = "TANK",
                description = "This is da best company.",
                website = "https://www.TankSouthy.com",
                employees = 1,
                sector = "Pets",
                industry = "Pets?",
                exchange = "NYSE"
            };
            var result = _companyProfileViewModel.ConvertToCompanyProfileViewModel(mockCompanyProfile);
            using (new AssertionScope())
            {
                result.Symbol.Should().Be(mockCompanyProfile.symbol);
                result.Should().NotBeNull();
            }
        }

        // Address test
    }
}
