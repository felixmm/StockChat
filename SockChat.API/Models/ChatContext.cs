using Microsoft.EntityFrameworkCore;
namespace SockChat.API.Models
{
    public class ChatContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Data Source=stockchat.db");
    }
}
