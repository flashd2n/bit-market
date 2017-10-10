using Flash.BitMarket.Web.Infrastructure.Providers;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Flash.BitMarket.Web.Infrastructure.SignalR
{
    public class QuoteTicker : IQuoteTicker
    {
        private readonly ITimerProvider timerProvider;
        private readonly ConcurrentDictionary<string, PriceQuote> currenciesRates;
        private readonly TimeSpan updateInterval;
        private readonly Timer timer;
        private readonly object updateStockPricesLock;
        private volatile bool updatingStockPrices;


        private readonly double _rangePercent = .002;
        private readonly Random _updateOrNotRandom = new Random();

        public QuoteTicker(IHubConnectionContext<dynamic> clients, ITimerProvider timerProvider)
        {
            this.currenciesRates = new ConcurrentDictionary<string, PriceQuote>();
            this.Clients = clients;
            this.timerProvider = timerProvider;
            this.updateInterval =  TimeSpan.FromMilliseconds(10000);
            this.updateStockPricesLock = new object();
            this.updatingStockPrices = false;

            // delete from here
            currenciesRates.Clear();
            var stocks = new List<PriceQuote>
            {
                new PriceQuote { Symbol = "MSFT", Price = 30.31m },
                new PriceQuote { Symbol = "APPL", Price = 578.18m },
                new PriceQuote { Symbol = "GOOG", Price = 570.30m }
            };
            stocks.ForEach(stock => currenciesRates.TryAdd(stock.Symbol, stock));
            // to here

            this.timer = this.timerProvider.GetTimer(UpdatePrices, null, updateInterval, updateInterval);
        }

        private IHubConnectionContext<dynamic> Clients { get; set; }

        public IEnumerable<PriceQuote> GetAllCurrencies()
        {
            return currenciesRates.Values;
        }

        private void UpdatePrices(object state)
        {
            lock (updateStockPricesLock)
            {
                if (!updatingStockPrices)
                {
                    updatingStockPrices = true;

                    // get new prices from API and broadcast
                    foreach (var stock in currenciesRates.Values)
                    {
                        if (TryUpdateStockPrice(stock))
                        {
                            BroadcastPrices(stock);
                        }
                    }
                    // end broadcast

                    updatingStockPrices = false;
                }
            }
        }

        private bool TryUpdateStockPrice(PriceQuote stock)
        {
            // Randomly choose whether to update this stock or not
            var r = _updateOrNotRandom.NextDouble();
            if (r > .1)
            {
                return false;
            }

            // Update the stock price by a random factor of the range percent
            var random = new Random((int)Math.Floor(stock.Price));
            var percentChange = random.NextDouble() * _rangePercent;
            var pos = random.NextDouble() > .51;
            var change = Math.Round(stock.Price * (decimal)percentChange, 2);
            change = pos ? change : -change;

            stock.Price += change;
            return true;
        }

        private void BroadcastPrices(PriceQuote stock)
        {
            Clients.All.updateStockPrice(stock);
        }

    }
}