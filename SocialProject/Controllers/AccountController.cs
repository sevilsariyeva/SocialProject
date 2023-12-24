using Microsoft.AspNetCore.Mvc;

namespace SocialProject.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View("Register");
        }
        public IActionResult Login()
        {
            return View("Login");
        }
        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }
    }
}
