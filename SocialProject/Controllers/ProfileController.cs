using Microsoft.AspNetCore.Mvc;

namespace SocialProject.WebUI.Controllers
{
    public class ProfileController : Controller
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
        public IActionResult HelpAndSupport()
        {
            return View("HelpAndSupport");
        }
        public IActionResult Notifications()
        {
            return View("Notifications");
        }
    }
}
