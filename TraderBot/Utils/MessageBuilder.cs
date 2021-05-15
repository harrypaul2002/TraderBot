using DSharpPlus.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TraderBot.Interfaces;
using TraderBot.Models;

namespace TraderBot.Utils
{
    public static class MessageBuilder
    {
        public static Dictionary<string, string> FormatCrypto(string symbol)
        {
            var dict = new Dictionary<string, string>();

            if (symbol.StartsWith("ADA"))
                dict.Add(ConfigurationManager.AppSettings.Get("CardanoLogo"), symbol.Replace("ADA", ""));
            else if (symbol.StartsWith("BTC"))
                dict.Add(ConfigurationManager.AppSettings.Get("BitcoinLogo"), symbol.Replace("BTC", ""));
            else if (symbol.StartsWith("ETH"))
                dict.Add(ConfigurationManager.AppSettings.Get("EtheriumLogo"), symbol.Replace("ETH", ""));

            return dict;
        }

        public static DiscordEmbedBuilder FormatPriceMessage(IPriceResponse response, string symbol, string exchange)
        {
            var embed = new DiscordEmbedBuilder();
            var percentString = string.Empty;
            if (decimal.Parse(response.PriceChangePercent) < 0)
                embed.Color = DiscordColor.Red;
            else
            {
                embed.Color = DiscordColor.DarkGreen;
                percentString = "+" + response.PriceChangePercent;
            }

            var formattedCrypto = MessageBuilder.FormatCrypto(symbol).FirstOrDefault();
            embed.Author = new DiscordEmbedBuilder.EmbedAuthor { Name = symbol.ToUpper(), IconUrl = formattedCrypto.Key };
            embed.Description = $"**{response.Price} {formattedCrypto.Value} *({percentString}%)***";
            embed.Footer = new DiscordEmbedBuilder.EmbedFooter { Text = $"Price on {char.ToUpper(exchange[0]) + exchange.Substring(1)}" };

            return embed;
        }
    }
}
