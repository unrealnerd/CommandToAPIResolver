using System.Collections.Generic;
using System.Threading.Tasks;
using iconic.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace iconic.web.ViewComponents
{
    public class ConversationListViewComponent : ViewComponent
    {
        private readonly ConversationContext _conversationContext;

        public ConversationListViewComponent(ConversationContext conversationContext)
        {
            _conversationContext = conversationContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _conversationContext.Conversations.ToListAsync();

            return View(items.OrderByDescending(i => i.Id));
        }
    }
}