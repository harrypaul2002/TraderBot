using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TraderBot.Models
{
    public struct BinancePriceResponse
    {
        [JsonProperty("price")]
        public string Price { get; set; }
    }
}
