using Microsoft.AspNetCore.Mvc;

namespace SocialProject.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Message()
        {
            return View("Message");
        } 
        public IActionResult LiveChat()
        {
            return View("LiveChat");
        }
    }
}
