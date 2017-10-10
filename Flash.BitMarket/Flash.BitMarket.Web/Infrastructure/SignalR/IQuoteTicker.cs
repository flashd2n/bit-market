using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash.BitMarket.Web.Infrastructure.SignalR
{
    public interface IQuoteTicker
    {
        IEnumerable<PriceQuote> GetAllCurrencies();
    }
}
