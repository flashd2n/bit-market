using System.Collections.Generic;

namespace Flash.BitMarket.Interfaces.Services
{
    public interface IPriceQuoteService : IService
    {
        IDictionary<string, double> Quotes { get; }

        void StartService();
    }
}
