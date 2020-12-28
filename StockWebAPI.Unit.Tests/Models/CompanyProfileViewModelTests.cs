using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using StockWebAPI.Models.AlphaVantage;
using StockWebAPI.Models.IEXCloud;
using StockWebAPI.ViewModels;

namespace StockWebAPI.Unit.Tests.Models
{
    class CompanyProfileViewModelTests
    {
        private readonly CompanyProfileViewModel _companyProfileViewModel = new CompanyProfileViewModel();
        
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
                exchange = "NYSE",
                zip = "12345"
            };
            var result = _companyProfileViewModel.ConvertToCompanyProfileViewModel(mockCompanyProfile);
            using (new AssertionScope())
            {
                result.Symbol.Should().Be(mockCompanyProfile.symbol);
                result.Address.ZipCode.Should().Be(mockCompanyProfile.zip);
                result.Should().NotBeNull();
            }
        }

        [Test]
        public void ConvertToCompanyProfileViewModel_CompanyKeyStats_ReturnsCompanyProfileViewModel()
        {
            CompanyKeyStats companyKeyStats = new CompanyKeyStats()
            {
                Name = "Tank Southy LLP",
                Symbol = "BONE",
                Description = "Woof Woof!",
                FullTimeEmployees = "23"
            };
            var result = _companyProfileViewModel.ConvertToCompanyProfileViewModel(companyKeyStats);
            using (new AssertionScope())
            {
                result.Symbol.Should().Be("BONE");
                result.Should().NotBeNull();
            }
        }

        [Test]
        public void ConvertToCompanyProfileViewModel_CompanyKeyStatsAddress_ReturnsViewModelAddress()
        {
            CompanyKeyStats companyKeyStats = new CompanyKeyStats()
            {
                Address = "One Apple Park Way, Cupertino, CA, United States, 95014",
                FullTimeEmployees = "2"
            };
            var result = _companyProfileViewModel.ConvertToCompanyProfileViewModel(companyKeyStats);
            using (new AssertionScope())
            {
                result.Address.ZipCode.Should().Be("95014");
                result.Address.State.Should().Be("CA");
            };
        }
    }
}
