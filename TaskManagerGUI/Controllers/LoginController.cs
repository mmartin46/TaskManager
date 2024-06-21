// John 3:5

using Microsoft.AspNetCore.Mvc;
using TaskManagerGUI.Models;
using TaskManagerGUI.Repositories;

namespace TaskManagerGUI.Controllers
{
    public class LoginController : Controller
    {
        [ViewData]
        public string? ErrorMessage { get; set; }
        private readonly IUserRepository _userRepository = null;
        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
        public async Task AuthenticateRegister([FromForm] RegisterModel registerModel)
        {
            ViewData["ErrorMessage"] = "";
            bool trueValidState = true;
            if (ModelState.IsValid)
            {
                if (!registerModel.Password.Equals(registerModel.ConfirmPassword))
                {
                    trueValidState = false;
                    ViewData["ErrorMessage"] = "Passwords don't match";  
                }
                if (!registerModel.Email.Equals(registerModel.ConfirmEmail))
                {
                    trueValidState = false;
                    ViewData["ErrorMessage"] += "\nEmails don't match";
                }

                if (trueValidState == true)
                {
                    /* Insert into database */
                    await _userRepository.Add(registerModel);
                }
            }
        }
    }
}