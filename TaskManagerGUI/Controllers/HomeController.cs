using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
