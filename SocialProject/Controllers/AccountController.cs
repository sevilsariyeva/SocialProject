using Microsoft.AspNetCore.Mvc;

namespace SocialProject.Controllers
{
    public class AccountController:Controller
    {
        public IActionResult MyProfile()
        {
            return View("MyProfile");
        }
        public IActionResult Friends()
        {
            return View("Friends");
        }
        public IActionResult Setting()
        {
            return View("Setting");
        }
        public IActionResult Privacy()
        {
            return View("Privacy");
        }
    }
}
