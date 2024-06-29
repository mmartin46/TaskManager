// John 3:5

using Microsoft.AspNetCore.Mvc;
using TaskManagerGUI.Constants;
using TaskManagerGUI.Models;
using TaskManagerGUI.Repositories;

namespace TaskManagerGUI.Controllers
{
    public class LoginController : IBaseController
    {
        [ViewData]
        public string? ErrorMessage { get; set; }
        private readonly IUserRepository _userRepository = null;
        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ViewResult Index(bool? didAuthenticate)
        {
            ViewBag.DidAuthenticate = didAuthenticate;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AuthenticateUser([FromForm] LoginModel loginModel)
        {
            List<LoginModel> users = await _userRepository.Get();
            bool userExists = (from user in users
                              where user.Username.Equals(loginModel.Username) &&
                                    user.Password.Equals(loginModel.Password)
                              select user).Any();

            if (userExists)
            {
                Username = loginModel.Username;
                return View();
            }


            return RedirectToAction(nameof(Index), new { didAuthenticate = false });
        }

       
        public ViewResult Register(bool? didAuthenticate, bool? userExists)
        {
            ViewBag.didAuthenticate = didAuthenticate;
            ViewBag.UserExists = userExists;
            return View();
        }

        public IActionResult Logout()
        {
            if (Username.Equals(ValueConstants.DefaultUsername))
            {
                return RedirectToAction(nameof(Index), new { didAuthenticate = false });
            }

            Username = ValueConstants.DefaultUsername;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AuthenticateRegister([FromForm] RegisterModel registerModel)
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
                    bool userExists = await _userRepository.Add(registerModel);
                    if (!userExists)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction(nameof(Register), new { didAuthenticate = true, userExists = true });
                    }
                }
            }
            return RedirectToAction(nameof(Register), new { didAuthenticate = false, userExists = false });
        }
    }
}