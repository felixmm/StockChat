using StockChat.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace StockChat.API.Helpers
{
    public class MessagesRepository
    {
        private readonly ChatContext dbContext;

        public MessagesRepository() : this(new ChatContext())
        { }

        public MessagesRepository(ChatContext context)
        {
            this.dbContext = context;
        }

        public Message Save(Message message)
        {
            // Adding the username to the message for ease of use when sending the New Message signal
            var user = this.dbContext.Users.First(u => u.Id == message.UserId);
            message.Username = user.Username;

            this.dbContext.Messages.Add(message);
            this.dbContext.SaveChanges();

            return message;
        }

        /// <summary>
        /// Gets the last messages specified ordered by date.
        /// </summary>
        /// <param name="take">The amount of messages to return. Defautls to last 50.</param>
        /// <returns>List of messages ordered by date saved.</returns>
        public List<Message> GetLastMessages(int take = 50)
        {
            return dbContext.Messages.OrderBy(m => m.Date).Take(take).ToList();
        }
    }
}
