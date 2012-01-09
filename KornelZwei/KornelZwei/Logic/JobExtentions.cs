using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KornelZwei.Logic
{
    public static class JobExtension
    {
        public static Job Create(this List<Job> jobList, Socket socket, int timestamp, Scheduler s)
        {
            Job job = new Job(socket, timestamp);
            jobList.Add(job);

            JobGenerationEvent e = new JobGenerationEvent(job, timestamp);
            s.AddEvent(e);

            return job;
        }



        public static void Kill(this List<Job> jobList, List<Job> killedJobs, Job job)
        {
            killedJobs.Add(job);
            jobList.Remove(job);
        }
    }
}
