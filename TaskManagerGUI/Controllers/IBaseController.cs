using Microsoft.AspNetCore.Mvc;
using TaskManagerGUI.Constants;

namespace TaskManagerGUI.Controllers
{
    public class IBaseController : Controller
    {
        public static string Username = ValueConstants.DefaultUsername;
    }
}
