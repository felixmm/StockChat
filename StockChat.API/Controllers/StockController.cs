using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockChat.Bot;

namespace StockChat.API.Controllers
{
    [Route("Stocks")]
    public class StockController : Controller
    {
        private readonly StockBot stockBot;

        public StockController()
        {
            this.stockBot = new StockBot();
        }

        [Route("{code}")]
        [HttpGet]
        public async Task<ActionResult<Quote>> GetStock(string code)
        {
            var quote = await this.stockBot.GetQuote(code);

            if(quote == null)
            {
                return BadRequest(quote);
            }

            return Ok(quote);
        }
    }
}