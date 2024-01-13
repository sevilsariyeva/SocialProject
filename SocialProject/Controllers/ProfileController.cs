using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> FriendRequests()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var friendRequests = _context.FriendRequests?
                .Where(fr => fr.ReceiverId == user.Id && fr.Status == "Pending")
                .Include(fr => fr.Sender)
                .ToList();

            ViewBag.User = new
            {
                ImageUrl = user.ImageUrl,
                Username = user.UserName,
                Email = user.Email
            };
            ViewBag.FriendRequests = friendRequests?.Select(fr => new
            {
                SenderName = fr.Sender?.UserName,
                SenderEmail = fr.Sender?.Email,
                SenderImage = fr.Sender?.ImageUrl
            });

            return RedirectToAction("Friends", "Profile");
        }
        public async Task<IActionResult> SendFriendRequest(string id)
        {
            var sender = await _userManager.GetUserAsync(HttpContext.User);
            var receiverUser = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (receiverUser != null)
            {
                _context.FriendRequests.Add(new FriendRequest
                {
                    //Content = $"{sender.UserName} send friend request at {DateTime.Now.ToLongDateString()}",
                    SenderId = sender.Id,
                    Sender = sender,
                    ReceiverId = id,
                    Status = "Pending"
                });
                receiverUser.HasRequestPending = true;
                await _context.SaveChangesAsync();
                await _userManager.UpdateAsync(receiverUser);
                return Ok();
            }
            return BadRequest();
        }
        public async Task<IActionResult> AcceptRequest(string userId, string senderId, int requestId)
        {
            var receiverUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var sender = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == senderId);

            if (receiverUser != null)
            {
                receiverUser.FriendRequests.Add(new FriendRequest
                {
                   // Content = $"{sender?.UserName} accepted friend request at ${DateTime.Now.ToLongDateString()}",
                    SenderId = sender?.Id,
                    Sender = sender,
                    ReceiverId = receiverUser.Id,
                    Status = "Notification"
                });

                var receiverFriend = new Friend
                {
                    OwnId = receiverUser.Id,
                    YourFriendId = sender?.Id,
                };

                var senderFriend = new Friend
                {
                    OwnId = sender?.Id,
                    YourFriendId = receiverUser.Id,
                };

                _context.Friends?.Add(senderFriend);
                _context.Friends.Add(receiverFriend);

                var request = await _context.FriendRequests.FirstOrDefaultAsync(r => r.Id == requestId);
                _context.FriendRequests.Remove(request);

                await _userManager.UpdateAsync(receiverUser);
                await _userManager.UpdateAsync(sender);


                await _context.SaveChangesAsync();
            }
            return Ok();
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
        public async Task<IActionResult> Followers()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = new
            {
                ImageUrl = user.ImageUrl,
                Username = user.UserName,
                Email = user.Email
            };
            return View("Followers");
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
        public async Task<IActionResult> Suggestion()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var users = await _context.Users
                .Where(u => u.Id != currentUser.Id)
                .ToListAsync();
            ViewBag.User = new
            {
                ImageUrl = currentUser.ImageUrl,
                Username = currentUser.UserName,
                Email = currentUser.Email
            };

            var myrequests = _context.FriendRequests.Where(r => r.ReceiverId == currentUser.Id);
            var status = "";
            var isDisabled = false;
            ViewBag.Users = new List<object>();

            foreach (var item in users)
            {
                var request = myrequests.FirstOrDefault(r => r.SenderId == item.Id && r.Status == "Pending");
                bool isPending = request?.ReceiverId == currentUser.Id ? true : false;
                if (request != null)
                {
                    item.HasRequestPending = true;
                    status = "Pending";
                    isDisabled = true;
                }
                else
                {
                    status = "Add Friend";
                }

                ViewBag.Users.Add(new
                {
                    Id = item.Id,
                    ImageUrl = item.ImageUrl,
                    Username = item.UserName,
                    Email = item.Email,
                    Status = status,
                    IsButtonDisabled= isDisabled,
                    IsPending= isPending
                });
            }

            return View("Suggestion");
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
