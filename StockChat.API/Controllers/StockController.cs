using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StockChat.API.Helpers;
using StockChat.Bot;
using System.Threading.Tasks;

namespace StockChat.API.Controllers
{
    [Route("Stocks")]
    [Authorize]
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
            if (quote == null)
            {
                return BadRequest(quote);
            }

            await _hub.Clients.All.SendAsync("sendStock", quote);
            return Ok(quote);
        }
    }
}