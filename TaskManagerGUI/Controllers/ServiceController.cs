using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManagerGUI.Models;
using TaskManagerGUI.Repositories;

namespace TaskManagerGUI.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceRepository? _serviceRepository = null;

        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ViewResult> Index([FromQuery] string? name)
        {
            var allServices = new List<ServiceModel>();
            if (_serviceRepository is not null)
            {
                allServices = await _serviceRepository.GetServices(name);
            }
            return View(allServices.Take(100).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> ConfigureService(string displayName, string status)
        {
            if (_serviceRepository is not null)
            {
                await _serviceRepository.ConfigureService(displayName, status);
            }
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public IActionResult QueryService([FromBody] ServiceModel service)
        {
            var url = Url.Action("Index", "Service", new { name = service.DisplayName });
            return Redirect(url ?? nameof(Index));
        }
    }
}
