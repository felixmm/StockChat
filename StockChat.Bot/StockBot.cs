using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockChat.Bot
{
    public class StockBot
    {
        private readonly string stockUrl = "https://stooq.com/q/l/?s={0}&f=sd2t2ohlcv&h&e=json";

        public StockBot()
        { }

        public async Task<Quote> GetQuote(string stockCode)
        {
            Quote quote = null;
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(string.Format(this.stockUrl, stockCode));
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                var quoteList = JsonConvert.DeserializeObject<QuoteList>(content);

                if(quoteList == null || !quoteList.Quotes.Any())
                {
                    return quote;
                }

                quote = quoteList.Quotes.First();
            }
            catch
            {
                //TODO: Error logger
            }

            return quote;
        }
    }
}
