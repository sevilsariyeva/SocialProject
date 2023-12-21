using Microsoft.AspNetCore.Mvc;

namespace SocialProject.WebUI.Controllers
{
    public class FeedController:Controller
    {
        public IActionResult Weather()
        {
            return View("Weather");
        }
    }
}
