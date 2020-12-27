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
        public int NumberOfEmployees { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }
        public string Exchange { get; set; }
        public CompanyProfileViewModel ConvertToCompanyProfileViewModel(CompanyProfile companyProfile)
        {
            return new CompanyProfileViewModel()
            {
                Name = companyProfile.companyName,
                Symbol = companyProfile.symbol,
                Description = companyProfile.description,
                CEO = companyProfile.CEO,
                Address = new Address()
                {
                    City = companyProfile.city,
                    StreetAddress = companyProfile.address,
                    State = companyProfile.state,
                    ZipCode = companyProfile.zip
                },
                WebsiteUrl = companyProfile.website,
                NumberOfEmployees = companyProfile.employees,
                Sector = companyProfile.sector,
                Industry = companyProfile.industry,
                Exchange = companyProfile.exchange
            };
        }
        public CompanyProfileViewModel ConvertToCompanyProfileViewModel(CompanyKeyStats companyKeyStats)
        {
            return new CompanyProfileViewModel()
            {
                Name = companyKeyStats.Name,
                Symbol = companyKeyStats.Symbol,
                Description = companyKeyStats.Description,
                Address = CompanyKeyStatsAddressConverter(companyKeyStats)
            };
        }
        private static Address CompanyKeyStatsAddressConverter(CompanyKeyStats companyKeyStats)
        {
            if (string.IsNullOrEmpty(companyKeyStats.Address))
            {
                return new Address();
            }
            string[] addressParts = companyKeyStats.Address.Split(", ");
            Address address = new Address()
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
