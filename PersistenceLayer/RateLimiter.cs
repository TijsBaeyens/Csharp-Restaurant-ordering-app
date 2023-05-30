using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer
{
    public class RateLimiter
    {
        private int requestCount;
        private Timer timer;

        public RateLimiter(int maxRequests, TimeSpan duration)
        {
            requestCount = 0;
            timer = new Timer(ResetRequestCount, null, duration, duration);
            MaxRequests = maxRequests;
        }

        public int MaxRequests { get; }

        public bool CanMakeRequest()
        {
            if (requestCount < MaxRequests)
            {
                requestCount++;
                return true;
            }

            return false;
        }

        private void ResetRequestCount(object state)
        {
            requestCount = 0;
        }
    }

}
