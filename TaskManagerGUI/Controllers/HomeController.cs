using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagerGUI.Models;
using TaskManagerGUI.Repositories;

namespace TaskManagerGUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProcessRepository _processRepository = null;
        [ViewData]
        public List<ProcessModel> ProcessList { get; set; }

        public HomeController(ProcessRepository processRepository) 
        { 
            _processRepository = processRepository;
        }
        public async Task<ViewResult> Index()
        {
            ProcessList = await _processRepository.GetProcessesByNameAsync();
            return View();
        }

    }
}
