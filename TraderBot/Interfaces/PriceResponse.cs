using System;
using System.Collections.Generic;
using System.Text;

namespace TraderBot.Interfaces
{
    public interface IPriceResponse
    {
        public string PriceChangePercent { get; set; }
        public string Price { get; set; }
    }
}
