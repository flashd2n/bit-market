using System;
using System.Threading;

namespace Flash.BitMarket.Web.Infrastructure.Providers
{
    public interface ITimerProvider
    {
        Timer GetTimer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period);
    }
}
