using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using TaskManagerGUI.Models;
using TaskManagerGUI.Repositories;

namespace TaskManagerGUI.Controllers
{
    public class StockController : IBaseController
    {

        private readonly IStockRepository _stockRepository = null;
        private readonly ISelectCompanyRepository _selectCompanyRepository = null;
        private readonly IConfiguration _configuration;

        [ViewData]
        public string StockName { get; set; }

        public StockController(IStockRepository stockRepository, ISelectCompanyRepository selectCompanyRepository, IConfiguration configuration)
        {
            _stockRepository = stockRepository;
            _selectCompanyRepository = selectCompanyRepository;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("/Stock/{company:alpha?}")]
        public async Task<ViewResult> Index(string? company)
        {
            SelectCompanyViewModel viewModel = new SelectCompanyViewModel();
            viewModel.Values = await _selectCompanyRepository.GetCompanySelectListItems();
            // If a company isn't provided?
            if (company == null)
            {
                StockName = _configuration.GetValue<string>("DefaultCompany") ?? "";
            }
            else
            {
                StockName = company;
            }


            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Wow(SelectCompanyViewModel viewModel)
        {
            return RedirectToAction(nameof(Index), new { company = viewModel.SelectedCompany });
        }


        [HttpPost]
        public IActionResult Search([FromForm] SearchRequestModel searchModel)
        {
            if (ModelState.IsValid)
            {
                StockName = searchModel.RequestName;
                return RedirectToAction(nameof(Index), new { company = searchModel.RequestName });
            }
            return RedirectToAction(nameof(Index), new { company = "IBM" });
        }


    }
}
