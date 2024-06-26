using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Management;
using System.Management.Automation;
using System.Text.Json;
using TaskManagerGUI.Constants;
using TaskManagerGUI.Hubs;
using TaskManagerGUI.Models;
using TaskManagerGUI.Repositories;

namespace TaskManagerGUI.Controllers
{
    public class HomeController : IBaseController
    {

        private readonly IProcessRepository? _processRepository = null;
        private readonly IMemoryRepository? _memoryRepository = null;

        private readonly IHubContext<MemoryStatsHub> _memoryStatsHubContext;
        private readonly IHubContext<ProcessHub> _processHubContext;

        private readonly List<Timer> _timers;
        private readonly object _lock = new object();

        private ILogger<IBaseController> _logger;

        [ViewData]
        public List<ProcessModel> ProcessList { get; set; }
        [ViewData]
        public List<MemoryModel> MemoryList { get; set; }


        public HomeController(IProcessRepository processRepository, 
                              IMemoryRepository memoryRepository,
                                IHubContext<MemoryStatsHub> memoryStatsHubContext,
                                IHubContext<ProcessHub> processHubContext,
                                ILogger<IBaseController> logger)
        {
            _processRepository = processRepository;
            _memoryRepository = memoryRepository;

            _memoryStatsHubContext = memoryStatsHubContext;
            _processHubContext = processHubContext;

            MemoryList = new List<MemoryModel>();

            _timers = new List<Timer>();

            InitializeTimers();
            _logger = logger;
        }



        private void InitializeTimers()
        {
            _timers.Add(new Timer(RefreshMemoryListAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(ValueConstants.RefreshRate)));
            _timers.Add(new Timer(RefreshProcessListAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(ValueConstants.RefreshRate)));
        }

        [HttpPost]
        public async Task<IActionResult> EndProcess([FromBody] ProcessRequestModel requestBody)
        {

            ProcessResponseModel responseModel = new ProcessResponseModel();
            try
            {
                Process[] processes = Process.GetProcessesByName(requestBody.ProcessToEnd);

                foreach (Process process in processes)
                {
                    try
                    {
                        process.CloseMainWindow();
                        await process.WaitForExitAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to stop process: {ex.Message}");
                    }
                }

                responseModel.Message = "Process Ended Successfully";
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                return StatusCode(500, responseModel);
            }
        }



        private void RefreshProcessListAsync(object state)
        {
            try
            {
                lock (_lock)
                {
                    if (ProcessList != null)
                    {
                        ProcessList.Clear();
                    }

                    if (_processRepository is not null)
                    {
                        ProcessList = _processRepository.GetProcessesByNameAsync().Result;
                        ProcessList = ProcessList.OrderByDescending(process => process.CPU).Take(90).ToList();
                        _processHubContext.Clients.All.SendAsync("UpdateProcesses", ProcessList);
                    }
                }
            }
            catch
            {
                _logger.LogInformation("Couldn't load from processes");
            }
        }

        private void RefreshMemoryListAsync(object state)
        {
            try
            {
                lock (_lock)
                {
                    if (_memoryRepository is not null)
                    {
                        var memoryModel = _memoryRepository.GetMemoryModelAsync().Result;
                        MemoryList.Add(memoryModel);
                    }
                    _memoryStatsHubContext.Clients.All.SendAsync("UpdateMemoryStats", MemoryList);
                }
            }
            catch
            {
                _logger.LogInformation("Couldn't load from memory");
            }
        }

        public async Task<ViewResult> Index()
        {
            if (_processRepository is not null)
            {
                ProcessList = await _processRepository.GetProcessesByNameAsync();
            }

            return View();
        }

        [Route("/MemoryStats")]
        public JsonResult MemoryStats()
        {
            return Json(MemoryList);
        }


    }
}
