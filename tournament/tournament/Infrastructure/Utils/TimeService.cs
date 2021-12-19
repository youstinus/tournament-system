using System;
using tournament.Services;

namespace tournament.Infrastructure
{
    public class TimeService : ITimeService
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
