using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialProject.Entities;

namespace SocialProject.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private CustomIdentityDbContext _context;

        public ProfileController(UserManager<CustomIdentityUser> userManager, CustomIdentityDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> MyProfile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
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
