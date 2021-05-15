using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TraderBot.Interfaces;

namespace TraderBot.Models
{
    public struct Binance24HourPriceResponse : IPriceResponse
    {
        [JsonProperty("priceChangePercent")]
        public string PriceChangePercent { get; set; }

        [JsonProperty("weightedAvgPrice")]
        public string Price { get; set; }       
    }
}
