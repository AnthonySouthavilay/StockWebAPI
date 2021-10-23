using StockWebAPI.Models;
using StockWebAPI.Models.AlphaVantage;
using StockWebAPI.Models.IEXCloud;

namespace StockWebAPI.ViewModels
{
    public class CompanyProfileViewModel
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public string CEO { get; set; }
        public Address Address { get; set; }
        public string WebsiteUrl { get; set; }
        public int? NumberOfEmployees { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }
        public string Exchange { get; set; }
        public CompanyProfileViewModel ConvertToCompanyProfileViewModel(IEXCompanyProfile companyProfile)
        {
            return new CompanyProfileViewModel()
            {
                Name = companyProfile.CompanyName,
                Symbol = companyProfile.Symbol,
                Description = companyProfile.Description,
                CEO = companyProfile.CEO,
                Address = new Address()
                {
                    City = companyProfile.City,
                    StreetAddress = companyProfile.Address,
                    State = companyProfile.State,
                    ZipCode = companyProfile.Zip
                },
                WebsiteUrl = companyProfile.Website,
                NumberOfEmployees = companyProfile.Employees,
                Sector = companyProfile.Sector,
                Industry = companyProfile.Industry,
                Exchange = companyProfile.Exchange
            };
        }
        public CompanyProfileViewModel ConvertToCompanyProfileViewModel(AlphaVantageCompanyKeyStats companyKeyStats)
        {
            return new CompanyProfileViewModel()
            {
                Name = companyKeyStats.Name,
                Symbol = companyKeyStats.Symbol,
                Description = companyKeyStats.Description,
                Address = CompanyKeyStatsAddressConverter(companyKeyStats),
                NumberOfEmployees = int.Parse(companyKeyStats.FullTimeEmployees),
                Exchange = companyKeyStats.Exchange,
                Sector = companyKeyStats.Sector,
                Industry = companyKeyStats.Industry
            };
        }
        private static Address CompanyKeyStatsAddressConverter(AlphaVantageCompanyKeyStats companyKeyStats)
        {
            if (string.IsNullOrEmpty(companyKeyStats.Address))
            {
                return new Address();
            }
            string[] addressParts = companyKeyStats.Address.Split(", ");
            Address address = new()
            {
                StreetAddress = addressParts[0],
                City = addressParts[1],
                State = addressParts[2],
                ZipCode = addressParts[4]
            };
            return address;
        }
    }
}
