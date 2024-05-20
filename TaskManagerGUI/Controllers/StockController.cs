using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using TaskManagerGUI.Models;
using TaskManagerGUI.Repositories;

namespace TaskManagerGUI.Controllers
{
    public class StockController : Controller
    {

        private readonly IStockRepository _stockRepository = null;
        private readonly IConfiguration _configuration;

        [ViewData]
        public string StockName { get; set; }

        public StockController(IStockRepository stockRepository, IConfiguration configuration)
        {
            _stockRepository = stockRepository;
            _configuration = configuration;
        }
        
        public async Task<ViewResult> Index(string? company)
        {
            CompanyModel stockModel = await _stockRepository.GetEarnings(company);
            // If a company isn't provided?
            if (company == null)
            {
                StockName = _configuration.GetValue<string>("DefaultCompany");
            }
            else
            {
                StockName = company;
            }
            return View();
        }

    }
}
