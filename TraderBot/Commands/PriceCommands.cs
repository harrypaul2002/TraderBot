using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using TraderBot.Models;
using DSharpPlus.Entities;

namespace TraderBot.Commands
{
    public class PriceCommands : BaseCommandModule
    {
        [Command("p")]
        [Description("Gets the price of a market with given symbol, currently hard coded to binance exchange only")]
        private async Task GetPrice(CommandContext ctx, [Description("Symbol of pair wanting to get price of eg 'BNBBTC'")] string symbol)
        {
            var client = new RestClient("https://api1.binance.com");
            var request = new RestRequest("/api/v3/avgPrice");
            request.AddParameter("symbol", symbol.ToUpper());
            var response = client.Get(request);
            var jsonContent = JsonConvert.DeserializeObject<BinancePriceResponse>(response.Content);

            await ctx.Channel.SendMessageAsync(embed: new DiscordEmbedBuilder
            {
                //add logic to get relevant logo
                Author = new DiscordEmbedBuilder.EmbedAuthor { Name = symbol.ToUpper(), IconUrl = "https://cryptologos.cc/logos/cardano-ada-logo.png?v=010" },
                Description = jsonContent.Price,
                Color = DiscordColor.Red,
            });
        }
    }
}