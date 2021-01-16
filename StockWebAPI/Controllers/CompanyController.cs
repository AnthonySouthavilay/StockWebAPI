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
        private CompanyService _companyService;
        private NewsService _newsService;
        private readonly HttpClient _httpClient;

        public CompanyController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        [Route("{companySymbol}/CompanySummary")]
        public async Task<CompanySummaryViewModel> GetCompanySummaryAsync(string companySymbol)
        {
            _companyService = new CompanyService(_httpClient);
            return await _companyService.GetCompanySummaryAsync(companySymbol);
        }

        [HttpGet]
        [Route("{companySymbol}/CompanyProfile")]
        public async Task<CompanyProfileViewModel> GetCompanyProfileAsync(string companySymbol)
        {
            _companyService = new CompanyService(_httpClient);
            return await _companyService.GetCompanyProfileAsync(companySymbol);
        }

        [HttpGet]
        [Route("{companySymbol}/CurrentNews")]
        public async Task<CompanyNewsViewModel[]> GetCurrentCompanyNewsAsync(string companySymbol)
        {
            _newsService = new NewsService(_httpClient);
            return await _newsService.GetCompanyNewsAsync(companySymbol);
        }

        [HttpGet]
        [Route("{companySymbol}/News/{startDate}/{endDate}")]
        public async Task<CompanyNewsViewModel[]> GetCompanyNewsAsync(string companySymbol, string startDate, string endDate)
        {
            _newsService = new NewsService(_httpClient);
            return await _newsService.GetCompanyNewsAsync(companySymbol, startDate, endDate);
        }
    }
}
