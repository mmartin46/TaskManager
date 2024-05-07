using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;
using TaskManagerGUI.Hubs;
using TaskManagerGUI.Models;
using TaskManagerGUI.Repositories;

namespace TaskManagerGUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProcessRepository _processRepository = null;
        private readonly MemoryRepository _memoryRepository = null;

        private readonly IHubContext<MemoryStatsHub> _memoryStatsHubContext;
        private readonly IHubContext<ProcessHub> _processHubContext;

        private readonly List<Timer> _timers;
        private readonly object _lock = new object();

        [ViewData]
        public List<ProcessModel> ProcessList { get; set; }
        [ViewData]
        public List<MemoryModel> MemoryList { get; set; }

        public HomeController(ProcessRepository processRepository, MemoryRepository memoryRepository,
                                IHubContext<MemoryStatsHub> memoryStatsHubContext,
                                IHubContext<ProcessHub> processHubContext) 
        { 
            _processRepository = processRepository;
            _memoryRepository = memoryRepository;
            _memoryStatsHubContext = memoryStatsHubContext;
            _processHubContext = processHubContext;

            MemoryList = new List<MemoryModel>();


            _timers = new List<Timer>();

            _timers.Add(new Timer(RefreshMemoryListAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(5)));
            _timers.Add(new Timer(RefreshProcessListAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(10)));
        }


        private async void RefreshProcessListAsync(object state)
        {
            try
            {
                ProcessList = await _processRepository.GetProcessesByNameAsync();
                await _processHubContext.Clients.All.SendAsync("UpdateProcesses", ProcessList);
            }
            catch (Exception ex)
            {

            }
        }

        private void RefreshMemoryListAsync(object state)
        {
            try
            {
                lock (_lock)
                {
                    var memoryModel = _memoryRepository.GetMemoryModelAsync().Result;
                    MemoryList.Add(memoryModel);
                }
                _memoryStatsHubContext.Clients.All.SendAsync("UpdateMemoryStats", MemoryList);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<ViewResult> Index()
        {
            ProcessList = await _processRepository.GetProcessesByNameAsync();

            return View();
        }

        [Route("/MemoryStats")]
        public JsonResult MemoryStats()
        {
            return Json(MemoryList);
        }


    }
}
