using Flash.BitMarket.Data;
using Flash.BitMarket.Interfaces.Services;
using Flash.BitMarket.Models;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace Flash.BitMarket.Services.Price_Quotes
{
    public class PriceQuoteService : IPriceQuoteService
    {
        private readonly HttpClient client;
        private readonly Timer aTimer;
        private IDictionary<string, double> result;
        private IDictionary<string, Quote> responseRaw;
        //private readonly MainDbContext db;

        public PriceQuoteService()
        {
            //this.db = new MainDbContext();
            this.client = new HttpClient();

            this.aTimer = new Timer(10000);

            this.result = new Dictionary<string, double>();
            this.responseRaw = new Dictionary<string, Quote>();
        }

        public IDictionary<string, double> Quotes
        {
            get { return this.result; }
        }

        public void StartService()
        {
            this.aTimer.Elapsed += OnTimedEvent;
            this.aTimer.AutoReset = true;
            this.aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            this.RunAsync().Wait();
        }

        private async Task RunAsync()
        {
            try
            {
                // Get the product
                var quotes = await this.GetQuotesAsync(@"https://min-api.cryptocompare.com/data/pricemulti?fsyms=BTC,ETH,LTC,DOGE,DASH&tsyms=USD");
                // do something with the quote                

                var json = JsonConvert.SerializeObject(quotes);

                //Clients.All.IncomingQuotes(json);
                //foreach (var key in quotes.Keys)
                //{
                //    var entry = new QuoteTest
                //    {
                //        Symbol = key,
                //        Price = quotes[key]
                //    };
                //    //db.Quotes.Add(entry);
                //    //db.SaveChanges();
                //}                

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                // log exception
            }

        }

        private async Task<IDictionary<string, double>> GetQuotesAsync(string path)
        {
            var response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                responseRaw = await response.Content.ReadAsAsync<IDictionary<string, Quote>>();

                result.Clear();

                foreach (var item in responseRaw.Keys)
                {
                    result.Add(item, responseRaw[item].USD);
                }

            }
            return result;
        }
    }
}
