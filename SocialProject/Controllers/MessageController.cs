using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var users = await _context.Users
                .Where(u => u.Id != currentUser.Id && u.IsOnline)
                .ToListAsync();
            ViewBag.User = new
            {
                ImageUrl = currentUser.ImageUrl,
                Username = currentUser.UserName,
                Email = currentUser.Email
            };
            ViewBag.Users = new List<object>();
            foreach (var item in users)
            {
                ViewBag.Users.Add(new
                {
                    Id = item.Id,
                    ImageUrl = item.ImageUrl,
                    Username = item.UserName,
                    Email = item.Email
                });
            }
            return View("LiveChat");
        }
        public async Task<IActionResult> GetChatHistory(string senderId, string receiverId)
        {
            try
            {
                var messages = await _context.Messages
                    .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                                (m.SenderId == receiverId && m.ReceiverId == senderId))
                    .OrderBy(m => m.DateTime)
                    .ToListAsync();

                return Json(messages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
