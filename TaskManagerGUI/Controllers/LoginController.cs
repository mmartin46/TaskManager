// John 3:5

using Microsoft.AspNetCore.Mvc;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Controllers
{
    public class LoginController : Controller
    {
        [ViewData]
        public string? ErrorMessage { get; set; }
        public LoginController()
        {

        }

        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AuthenticateUser([FromForm] LoginModel loginModel)
        {

            return Json("{ }");
        }

       
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AuthenticateRegister([FromForm] RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                if (!registerModel.Password.Equals(registerModel.ConfirmPassword))
                {
                    ViewData["ErrorMessage"] = "Passwords don't match";  
                }
                else
                {
                    /* Insert into database */
                }
            }

            return Json("{ }");
        }
    }
}