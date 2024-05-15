using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using TaskManagerGUI.Models;
using TaskManagerGUI.Repositories;

namespace TaskManagerGUI.Controllers
{
    public class StockController : Controller
    {

        [ViewData]
        public StockModel[] StockNews { get; set; }
        private readonly StockRepository _stockRepository = null;

        public StockController(StockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        
        public async Task<ViewResult> Index()
        {
            ViewData["StockNews"] = await _stockRepository.ProcessStockApi("IBM");
            return View();
        }

    }
}
