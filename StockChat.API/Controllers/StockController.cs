using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StockChat.API.Helpers;
using StockChat.Bot;

namespace StockChat.API.Controllers
{
    [Route("Stocks")]
    public class StockController : Controller
    {
        private readonly StockBot stockBot;
        private readonly IHubContext<MessagesHub> _hub;

        public StockController(IHubContext<MessagesHub> hub)
        {
            this.stockBot = new StockBot();
            this._hub = hub;
        }

        [Route("{code}")]
        [HttpGet]
        public async Task<ActionResult<Quote>> GetStock(string code)
        {
            var quote = await this.stockBot.GetQuote(code);
            await _hub.Clients.All.SendAsync("sendStock", quote);

            if (quote == null)
            {
                return BadRequest(quote);
            }

            return Ok(quote);
        }
    }
}