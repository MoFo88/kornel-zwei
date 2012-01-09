using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KornelZwei.Logic
{
    class QueueFifo : Queue
    { 
        public QueueFifo(Scheduler s, int size) : base(s, size)
        {
            Name = "QFifo" + Id; 
        }

        public override Job Get()
        {
            Job job = null;
            
            if (Count > 0)
            {
                job = JobList.First();
                JobList.RemoveAt(0);
                AddEventGet();
                job.GetQueueTimeForQueue(this).stop = scheduler.timestamp;
            }
            return job;
        }

        public override Job Peak()
        {
            return JobList.First();
        }

        public override bool Put(Job job)
        {
            return base.Put(job);
        }
    }
}
