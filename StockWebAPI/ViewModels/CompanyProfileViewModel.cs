using StockWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebAPI.ViewModels
{
    public class CompanyProfileViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CEO { get; set; }
        public Address Address { get; set; }
        public string WebsiteUrl { get; set; }
        public string NumberOfEmployees { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }
        public string Exchange { get; set; }

        public CompanyProfileViewModel ConvertToCompanyProfileViewModel(CompanyProfile companyProfile)
        {
            return new CompanyProfileViewModel()
            {
                Name = companyProfile.companyName,
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
                //NumberOfEmployees = companyProfile.employees,
                Sector = companyProfile.sector,
                Industry = companyProfile.industry,
                Exchange = companyProfile.exchange
            };
        }
    }
}
