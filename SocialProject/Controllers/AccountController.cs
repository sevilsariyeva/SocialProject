using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SocialProject.Entities;
using SocialProject.WebUI.Models;

namespace SocialProject.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<CustomIdentityRole> _roleManager;
        private SignInManager<CustomIdentityUser> _signInManager;
        private IWebHostEnvironment _webHost;
        private CustomIdentityDbContext _context;
        private readonly IPasswordHasher<CustomIdentityUser> _passwordHasher;

        public AccountController(
           UserManager<CustomIdentityUser> userManager,
           RoleManager<CustomIdentityRole> roleManager,
           IPasswordHasher<CustomIdentityUser> passwordHasher,
           CustomIdentityDbContext context,
           IWebHostEnvironment webHost,
           SignInManager<CustomIdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _context = context;
            _webHost = webHost;
        }
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("Password", "Please enter a password");
                    return View(model);
                }
                CustomIdentityUser user = new CustomIdentityUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    ImageUrl = model.ImageUrl,
                };

                user.PasswordHash = _passwordHasher.HashPassword(user, model.Password); 

                IdentityResult result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
            }

            return View(model);
        }
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
