﻿using Microsoft.AspNetCore.Identity;
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
                SenderImage=fr.Sender?.ImageUrl
            });

            return RedirectToAction("Friends", "Profile"); 
        }
        public async Task<IActionResult> SendFriendRequest(string id)
        {
            var sender = await _userManager.GetUserAsync(HttpContext.User);
            var receiverUser = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (receiverUser != null)
            {
                _context.FriendRequests?.Add(new FriendRequest
                {
                    //Content = $"{sender.UserName} send friend request at {DateTime.Now.ToLongDateString()}",
                    SenderId = sender.Id,
                    Sender = sender,
                    ReceiverId = id,
                    Status = "Pending"
                });
                await _context.SaveChangesAsync();
                await _userManager.UpdateAsync(receiverUser);
                return Ok();
            }
            return BadRequest();
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
            var usersExceptCurrentUser = await _userManager.Users
                .Where(u => u.Id != currentUser.Id) 
                .ToListAsync();

            ViewBag.User = new
            {
                ImageUrl = currentUser.ImageUrl,
                Username = currentUser.UserName,
                Email = currentUser.Email
            };

            ViewBag.Users = usersExceptCurrentUser.Select(user => new
            {
                Id=user.Id,
                ImageUrl = user.ImageUrl,
                Username = user.UserName,
                Email = user.Email,
            });

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
