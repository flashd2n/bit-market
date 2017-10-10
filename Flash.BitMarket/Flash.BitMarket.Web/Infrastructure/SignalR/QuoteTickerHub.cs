using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Flash.BitMarket.Web.Infrastructure.SignalR
{
    [HubName("quoteTickerMainHub")]
    public class QuoteTickerHub : Hub
    {
        private readonly IQuoteTicker quoteTicker;

        public QuoteTickerHub(IQuoteTicker quoteTicker)
        {
            this.quoteTicker = quoteTicker;
        }

        public IEnumerable<PriceQuote> GetAllStocks()
        {
            return quoteTicker.GetAllCurrencies();
        }
    }
}