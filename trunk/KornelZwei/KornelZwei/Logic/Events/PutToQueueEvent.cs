using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KornelZwei.Logic
{
    class PutToQueueEvent : Event
    {
        public Queue queue;

        public PutToQueueEvent(int timestamp, Queue queue)
            : base(timestamp)
        {
            this.queue = queue;
        }
    }
}
