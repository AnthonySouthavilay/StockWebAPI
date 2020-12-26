using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockWebAPI.Service;
using StockWebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockWebAPI.Controllers
{
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private CompanyService _companyService;
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
    }
}
