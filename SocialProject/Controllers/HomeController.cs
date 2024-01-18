using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialProject.Entities;
using SocialProject.WebUI.Models;
using System.Diagnostics;

namespace SocialProject.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private CustomIdentityDbContext _context;

        public HomeController(UserManager<CustomIdentityUser> userManager, CustomIdentityDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = new
            {
                ImageUrl = user.ImageUrl,
                Username = user.UserName,
                Email = user.Email
            };
            return View();
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
       
            [HttpPost]
            public IActionResult CreatePost([FromBody] PostViewModel postModel)
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(postModel);
            }

        public async Task<List<CustomIdentityUser>> GetAllFriends()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var allFriends = _context.Friends
                .Where(f => f.OwnId == user.Id)
                .Select(f => f.YourFriend)
                .ToList();

            return allFriends;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}