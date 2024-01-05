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
            ViewBag.User = new
            {
                ImageUrl = user.ImageUrl,
                Username = user.UserName,
                Email = user.Email
            };
            return View("MyProfile");
        }
        public async Task<IActionResult> Friends()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = new
            {
                ImageUrl = user.ImageUrl,
                Username = user.UserName,
                Email = user.Email
            };
            return View("Friends");
        }
        public async Task<IActionResult> Setting()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = new
            {
                ImageUrl = user.ImageUrl,
                Username = user.UserName,
                Email = user.Email
            };
            return View("Setting");
        }
        public async Task<IActionResult> Privacy()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = new
            {
                ImageUrl = user.ImageUrl,
                Username = user.UserName,
                Email = user.Email
            };
            return View("Privacy");
        }
        public async Task<IActionResult> HelpAndSupport()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = new
            {
                ImageUrl = user.ImageUrl,
                Username = user.UserName,
                Email = user.Email
            };
            return View("HelpAndSupport");
        }
        public async Task<IActionResult> Notifications()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = new
            {
                ImageUrl = user.ImageUrl,
                Username = user.UserName,
                Email = user.Email
            };
            return View("Notifications");
        }
    }
}
