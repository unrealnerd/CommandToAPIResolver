using System.Threading.Tasks;
using iconic.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace iconic.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ConversationContext _conversationContext;

        public HomeController(ConversationContext conversationContext)
        {
            _conversationContext = conversationContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string message)
        {
            _conversationContext.Conversations.Add(new Conversation{ Message = message, Reply = "Some Reply" });
            await _conversationContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}