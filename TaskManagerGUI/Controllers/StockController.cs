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
        private readonly IStockRepository _stockRepository = null;

        public StockController(IStockRepository stockRepository)
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
