using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;
using TraderBot.Models;
using TraderBot.Utils;

namespace TraderBot.CommandControllers.Binance
{
    public class PriceController
    {
        private readonly RestClient Client = new RestClient("https://api1.binance.com");

        public async Task GetPrice(CommandContext ctx, 
            [Description("Symbol of pair wanting to get price of eg 'BNBBTC'")] string symbol, string exchange)
        {          
            var request = new RestRequest("/api/v3/ticker/24hr");
            request.AddParameter("symbol", symbol.ToUpper());
            var response = Client.Get(request);

            if(response.Content.Contains("Invalid symbol"))
                await ctx.Channel.SendMessageAsync($"Invalid symbol - {symbol}").ConfigureAwait(false);

            var embed = MessageBuilder.FormatPriceMessage(
                JsonConvert.DeserializeObject<Binance24HourPriceResponse>(response.Content), symbol, exchange);
           
            await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
        }
    }
}
