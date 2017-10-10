using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flash.BitMarket.Web.Infrastructure.SignalR
{
    public class PriceQuote
    {
        private decimal _price;

        public string Symbol { get; set; }

        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (_price == value)
                {
                    return;
                }

                _price = value;
            }
        }
    }
}