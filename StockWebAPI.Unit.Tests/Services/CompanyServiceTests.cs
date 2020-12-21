using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using StockWebAPI.ViewModels;
using StockWebAPI.Service;

namespace StockWebAPI.Unit.Tests.Services
{
    public class CompanyServiceTests
    {
        private CompanyService sut = new CompanyService();

        [Test]
        public void GetCompanyProfile_ValidSymbol_ReturnsCompanyProfileViewModel()
        {
            string symbol = "TANK";
            CompanyProfileViewModel companyProfile = sut.GetCompanyProfile(symbol);
            companyProfile.Should().NotBeNull();
        }

        [TestCase("T@NK")]
        [TestCase("T@N3K")]
        [TestCase("@!NK")]
        public void GetCompanyProfile_InvalidSymbol_ReturnsArgumentException(string invalidSymbol)
        {
            Action act = () => sut.GetCompanyProfile(invalidSymbol);
            act.Should().Throw<ArgumentException>();
        }

        [TestCase(" TANK")]
        [TestCase("TN3K ")]
        [TestCase(null)]
        public void GetCompanyProfile_SymbolHasWhiteSpaceOrIsNull_ReturnsArgumentException(string symbol)
        {
            Action act = () => sut.GetCompanyProfile(symbol);
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetCompanyProfile_EmptySymbol_ReturnsArgumentException()
        {
            Action act = () => sut.GetCompanyProfile("");
            act.Should().Throw<ArgumentException>();
        }
    }
}
