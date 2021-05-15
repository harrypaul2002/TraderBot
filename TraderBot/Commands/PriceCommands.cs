using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace TraderBot.Commands
{
    public class PriceCommands : BaseCommandModule
    {       
        [Command("p")]
        [Description("Gets the price of a market with given symbol, currently hard coded to binance exchange only")]
        private async Task GetPrice(CommandContext ctx, [Description("Symbol of pair wanting to get price of eg 'BNBBTC'")] string symbol, string exchange)
        {            
            switch (exchange.ToLower()) 
            {
                case "binance":
                    var controller = new CommandControllers.Binance.PriceController();
                    await controller.GetPrice(ctx, symbol, exchange);
                    return;
                default:
                    await ctx.Channel.SendMessageAsync($"Exchange: {exchange} not supported").ConfigureAwait(false);
                    return;
            }            
        }        
    }
}
