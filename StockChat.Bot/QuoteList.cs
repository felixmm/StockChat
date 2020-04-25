using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockChat.Bot
{
    public class QuoteList
    {
        [JsonPropertyAttribute("symbols")]
        public List<Quote> Quotes { get; set; }
    }
}
