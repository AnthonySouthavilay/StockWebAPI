using Microsoft.AspNetCore.Mvc;
using StockWebAPI.Service;
using StockWebAPI.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockWebAPI.Controllers
{
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public CompanyController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        [Route("{companySymbol}/CompanySummary")]
        public async Task<CompanySummaryViewModel> GetCompanySummaryAsync(string companySymbol)
        {
            var _companyService = new CompanyService(_httpClient);
            return await _companyService.GetCompanySummaryAsync(companySymbol);
        }

        [HttpGet]
        [Route("{companySymbol}/CompanyProfile")]
        public async Task<CompanyProfileViewModel> GetCompanyProfileAsync(string companySymbol)
        {
            var _companyService = new CompanyService(_httpClient);
            return await _companyService.GetCompanyProfileAsync(companySymbol);
        }
    }
}
