using Microsoft.AspNetCore.Mvc;
using StockWebAPI.Service;
using StockWebAPI.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockWebAPI.Controllers
{
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public NewsController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        [Route("{companySymbol}/CurrentNews")]
        public async Task<CompanyNewsViewModel[]> GetCurrentCompanyNewsAsync(string companySymbol)
        {
            var _newsService = new NewsService(_httpClient);
            return await _newsService.GetCurrentCompanyNewsAsync(companySymbol);
        }

        [HttpGet]
        [Route("{companySymbol}/News/{startDate}/{endDate}")]
        public async Task<CompanyNewsViewModel[]> GetCompanyNewsByDateRangeAsync(string companySymbol, string startDate, string endDate)
        {
            var _newsService = new NewsService(_httpClient);
            return await _newsService.GetCompanyNewsByDateRangeAsync(companySymbol, startDate, endDate);
        }
    }
}
