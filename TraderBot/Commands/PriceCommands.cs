using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using TraderBot.Models;
using DSharpPlus.Entities;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Skender.Stock.Indicators;
using System.Linq;
using System;

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

        [Command("i")]
        [Description("Gets the price of a market with given symbol, currently hard coded to binance exchange only")]
        private async Task GetIndicator(CommandContext ctx, [Description("Symbol of pair wanting to get indicator for eg 'BNBBTC'")] string symbol,
            [Description("Time intervals for candles eg '1h'")] string interval,
            [Description("How many candle sticks to gather eg 10")] string limit,
            [Description("Indicator name eg 'ma' for moving average ")] string indicatorType)
        {
            try
            {
                var client = new RestClient("https://api1.binance.com");
                var request = new RestRequest("/api/v3/klines");
                request.AddParameter("symbol", symbol.ToUpper());
                request.AddParameter("interval", interval);

                var limits = limit.Split(",");
                limits = limits.OrderBy(value => Convert.ToInt32(value)).ToArray<string>();

                request.AddParameter("limit", (int.Parse(limits.Last())).ToString());
            
            
                var response = client.Get(request);
           
                var jsonContent = JArray.Parse(response.Content); 
                List<Quote> quotes = new List<Quote>();

                foreach (var item in jsonContent)
                {
                    quotes.Add(new Quote()
                    {
                        Open = (decimal)item[1],
                        High = (decimal)item[2],
                        Low = (decimal)item[3],
                        Close = (decimal)item[4],
                        Volume = (decimal)item[5],
                    }
                    ) ;

                }

                string indicatorResult = "";


                indicatorType = indicatorType.ToLower();
                try
                {
                    switch (indicatorType)
                    {
                        case "ma": // only support simple moving average for now

                            for (int i = 0; i < limits.Length; i++)
                            {
                                indicatorResult += $"Moving Average {(limits[i])}: " + Indicator.GetSma(quotes, int.Parse(limits[i])).Last().Sma.ToString() + "\n";
                            }

                            break;
                        default:
                            indicatorResult = "indictator type not found";
                            break;
                    }
                }
                catch(System.Exception e)
                {
                    indicatorResult = e.Message;
                }

                await ctx.Channel.SendMessageAsync(embed: new DiscordEmbedBuilder
                {
                    //add logic to get relevant logo
                    Author = new DiscordEmbedBuilder.EmbedAuthor { Name = symbol.ToUpper(), IconUrl = "https://cryptologos.cc/logos/cardano-ada-logo.png?v=010" },
                    Description = indicatorResult,
                    Color = DiscordColor.Red,
                });
            }
            catch(System.Exception e)
            {
                await ctx.Channel.SendMessageAsync(embed: new DiscordEmbedBuilder
                {
                    //add logic to get relevant logo
                    Author = new DiscordEmbedBuilder.EmbedAuthor { Name = symbol.ToUpper(), IconUrl = "https://cryptologos.cc/logos/cardano-ada-logo.png?v=010" },
                    Description = e.Message,
                    Color = DiscordColor.Red,
                });
            }
            
            
        }
    }
}