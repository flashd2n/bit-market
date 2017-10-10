using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Flash.BitMarket.Web.Infrastructure.Providers
{
    public class TimerProvider : ITimerProvider
    {
        public Timer GetTimer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
        {
            return new Timer(callback, state, dueTime, period);
        }
    }
}