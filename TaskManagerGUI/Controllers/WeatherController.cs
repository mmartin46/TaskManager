using Microsoft.AspNetCore.Mvc;

namespace TaskManagerGUI.Controllers
{
    public class WeatherController : Controller
    {

        public ViewResult Index()
        {
            return View();
        }
    }
}
