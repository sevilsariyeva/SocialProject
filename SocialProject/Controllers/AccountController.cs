using Microsoft.AspNetCore.Mvc;

namespace SocialProject.Controllers
{
    public class AccountController : Controller
    {
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
