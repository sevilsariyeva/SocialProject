using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialProject.Entities;

namespace SocialProject.WebUI.Controllers
{
    public class MessageController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private CustomIdentityDbContext _context;

        public MessageController(UserManager<CustomIdentityUser> userManager, CustomIdentityDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Message()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = new
            {
                ImageUrl = user.ImageUrl,
                Username = user.UserName,
                Email = user.Email
            };
            return View("Message");
        }
        public async Task<IActionResult> LiveChat()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = new
            {
                ImageUrl = user.ImageUrl,
                Username = user.UserName,
                Email = user.Email
            };
            return View("LiveChat");
        }
    }
}
