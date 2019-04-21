using Microsoft.EntityFrameworkCore;

namespace iconic.web.Models
{
    public class ConversationContext : DbContext
    {
        public ConversationContext(DbContextOptions<ConversationContext> options) : base(options)
        {

        }
        public DbSet<Conversation> Conversations { get; set; }

    }
}