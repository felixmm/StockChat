using System;
namespace StockChat.API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Text { get; set; }

        //public virtual User User { get; set; }
        // Manually handling the relation between the User and the Message since SQLite doesn't support ForeignKeys
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
