using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StockChat.API.Helpers;
using StockChat.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockChat.API.Controllers
{
    [Route("Messages")]
    public class MessagesController : Controller
    {
        private readonly MessagesRepository repo;
        private readonly IHubContext<MessagesHub> _hub;

        public MessagesController(IHubContext<MessagesHub> hub)
        {
            repo = new MessagesRepository();
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

            var savedMessage = this.repo.Save(message);
            await _hub.Clients.All.SendAsync("sendMessage", savedMessage);

            return Ok(savedMessage);
        }
    }
}