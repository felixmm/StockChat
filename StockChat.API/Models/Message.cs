using System;
namespace StockChat.API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Text { get; set; }
    }
}
