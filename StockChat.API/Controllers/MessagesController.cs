using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StockChat.API.Helpers;
using StockChat.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockChat.API.Controllers
{
    [Route("Messages")]
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly MessagesRepository repo;
        private readonly IHubContext<MessagesHub> _hub;

        public MessagesController(ChatContext context, IHubContext<MessagesHub> hub)
        {
            repo = new MessagesRepository(context);
            this._hub = hub;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<List<Message>>> GetLastMessages()
        {
            var messages = repo.GetLastMessages();
            return Ok(messages);
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult<Message>> SendMessage([FromBody]Message message)
        {
            if(message == null || string.IsNullOrEmpty(message.Text))
            {
                return BadRequest(message);
            }

            // Due to the SQLite ForeignKey constraing, manually adding the Message -> User relation
            message.UserId = int.Parse(HttpContext.User.Identity.Name);
            var savedMessage = this.repo.Save(message);
            await _hub.Clients.All.SendAsync("sendMessage", savedMessage);

            return Ok(savedMessage);
        }
    }
}