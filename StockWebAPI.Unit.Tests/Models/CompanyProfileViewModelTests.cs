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
            IEXCompanyProfile mockCompanyProfile = new IEXCompanyProfile()
            {
                CompanyName = "Tank Southy LLC",
                Symbol = "TANK",
                Description = "This is da best company.",
                Website = "https://www.TankSouthy.com",
                Employees = 1,
                Sector = "Pets",
                Industry = "Pets?",
                Exchange = "NYSE",
                Zip = "12345"
            };
            var result = _companyProfileViewModel.ConvertToCompanyProfileViewModel(mockCompanyProfile);
            using (new AssertionScope())
            {
                result.Symbol.Should().Be(mockCompanyProfile.Symbol);
                result.Address.ZipCode.Should().Be(mockCompanyProfile.Zip);
                result.Should().NotBeNull();
            }
        }

        [Test]
        public void ConvertToCompanyProfileViewModel_CompanyKeyStats_ReturnsCompanyProfileViewModel()
        {
            AlphaVantageCompanyKeyStats companyKeyStats = new AlphaVantageCompanyKeyStats()
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
            AlphaVantageCompanyKeyStats companyKeyStats = new AlphaVantageCompanyKeyStats()
            {
                Address = "One Apple Park Way, Cupertino, CA, United States, 95014",
                FullTimeEmployees = "2"
            };
            var result = _companyProfileViewModel.ConvertToCompanyProfileViewModel(companyKeyStats);
            using (new AssertionScope())
            {
                result.Address.ZipCode.Should().Be("95014");
                result.Address.State.Should().Be("CA");
            }
        }
    }
}
