using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TraderBot.Models
{

    public struct Root
    {
        public List<List<string>> Result { get; set; }
    }

    class test
    {
       // [JsonArray]
        public BinanceCandlestickResponse[] BinanceCandlestickResponse { get; set; }
    }
    [JsonArray]
    class BinanceCandlestickResponse
    {

        [JsonProperty("Open time")]
        public string Opentime { get; set; }

        [JsonProperty("Open")]
        public string Open { get; set; }

        [JsonProperty("High")]
        public string High { get; set; }

        [JsonProperty("Low")]
        public string Low { get; set; }

        [JsonProperty("Close")]
        public string Close { get; set; }

        [JsonProperty("Volume")]
        public string Volume { get; set; }

        [JsonProperty("Close time")]
        public string Closetime { get; set; }

        [JsonProperty("Quote asset volume")]
        public string Quoteassetvolume { get; set; }

        [JsonProperty("Number of trades")]
        public string Numberoftrades { get; set; }

        [JsonProperty("Taker buy base asset volume")]
        public string Takerbuybaseassetvolume { get; set; }

        [JsonProperty("Taker buy quote asset volume")]
        public string Takerbuyquoteassetvolume { get; set; }

        [JsonProperty("Ignore")]
        public string Ignore { get; set; }
    }
}
