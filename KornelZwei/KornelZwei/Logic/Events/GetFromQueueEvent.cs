using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KornelZwei.Logic
{
    class GetFromQueueEvent : Event
    {
        public int timestamp;
        public Queue queue;

        public GetFromQueueEvent(int timestamp, Queue queue)
            : base(timestamp)
        {
            this.timestamp = timestamp;
            this.queue = queue;
        }
    }
}
