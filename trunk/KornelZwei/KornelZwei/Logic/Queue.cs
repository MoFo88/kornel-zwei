using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KornelZwei.Logic
{
    public abstract class Queue
    {
        public static int lastId = 0;
        public int Id { get; set; }
        public String Name { get; set; }
        public List<Job> JobList {get; set;}

        public Scheduler scheduler;
        
        public Queue(Scheduler s, int size)
        {
            ++lastId;
            Id = lastId;
            scheduler = s;
            Size = size;
            JobList = new List<Job>();
        }

        public override string ToString()
        {
            String s = Name + ": " + this.Count +"/"+ this.Size;

            return s;
        }

        public abstract Job Get();

        public abstract Job Peak();

        public virtual bool Put(Job job)
        {
            if (!IsFull)
            {
                if (!JobList.Contains(job)) JobList.Add(job);
                AddEventPut();
                
                job.GetQueueTimeForQueue(this).start = scheduler.timestamp;
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Size { get; set; }

        public int Count { get { return JobList.Count; } }

        public bool IsFull { get { if (Count >= Size) return true; return false; } }

        public bool IsEmpty { get { if (JobList.Count == 0) return true; return false; } }

        public void AddEventPut()
        {
            PutToQueueEvent ev = new PutToQueueEvent(scheduler.timestamp, this);
            scheduler.AddEvent(ev);
        }

        public void AddEventGet()
        {
            GetFromQueueEvent ev = new GetFromQueueEvent(scheduler.timestamp, this);
            scheduler.AddEvent(ev);
        }
       
    }
}
