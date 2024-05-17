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
        private readonly IConfiguration configuration;

        [ViewData]
        public string StockName { get; set; }

        public StockController(IStockRepository stockRepository, IConfiguration _configuration)
        {
            _stockRepository = stockRepository;
            configuration = _configuration;
        }
        
        public async Task<ViewResult> Index()
        {
            // If a company isn't provided?
            StockName = configuration["DefaultCompany"];
            return View();
        }

    }
}
