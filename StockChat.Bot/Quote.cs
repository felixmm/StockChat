using Newtonsoft.Json;
using System;

namespace StockChat.Bot
{
    public class Quote
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        [JsonPropertyAttribute("close")]
        public decimal Price { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public int Volume { get; set; }
    }
}
