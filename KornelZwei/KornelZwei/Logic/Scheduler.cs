using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using KornelZwei.Logic.MyMath;
using MathNet.Numerics.Distributions;

namespace KornelZwei.Logic
{
    public class Scheduler
    {
        public int timestamp;
         
        public List<Job> jobList;
        public Socket socket;
        public List<Event> eventList;
        public List<Job> killedJobsList;

        public bool SystemEmpty 
        { 
            get
            {
                if (eventList.Count > 0) return false;
                return true;
            } 
        }

        //
        //for stats
        public List<QueueSize> queueSize = new List<QueueSize>();
        
        public SimForm form;

        public void AddEvent(Event ev)
        {
            eventList.Add(ev);
        }

        public bool CheckIfGenerateJob(int prob)
        {
            DiscreteUniformDistribution uniformDistr = new DiscreteUniformDistribution(0, 60);
            int nextRandom = uniformDistr.NextInt32();
 
            if ( nextRandom < prob ) return true;
            return false;
        }

        public Scheduler()
        {
            socket = null;
            eventList = new List<Event>();
            jobList = new List<Job>();
            killedJobsList = new List<Job>();
            this.form = new SimForm(this);
            timestamp = 0;
        }

        public void JobGeneration()
        {
            if (CheckIfGenerateJob(Const.CAR_FREQ))
            {
                Job job = jobList.Create( socket, this.timestamp, this);

                //
                //tell
                form.Notify(timestamp + ". " + "Zadanie dodane: " + job.ToString());
            }

            //if (CheckIfGenerateJob(Const.JOB_UNIFORM_GENERATE_PROBABILITY))
            //{
            //    Job job = jobList.Create(new UniformDistr(Const.UNIFORM_MIN, Const.UNIFORM_MAX), socketList, this.timestamp, this);

            //    //
            //    //tell
            //    form.Notify(timestamp + ". " + "Zadanie dodane: " + job.ToString());
            //}

            //if (CheckIfGenerateJob(Const.JOB_EXPONENTIAL_GENERATE_PROBABILITY))
            //{
            //    Job job = jobList.Create(new ExponentialDistribution(Const.EXPONENTIAL_RATE), socketList, this.timestamp, this);

            //    //
            //    //tell
            //    form.Notify(timestamp + ". " + "Zadanie dodane: " + job.ToString());
            //}
        }

        public void MakeStep()
        {
            //next step
            timestamp++;

            form.Notify(this.timestamp + "\n", 1);

            JobGeneration();

            //
            //obsługa zdarzeń
            while (eventList.Where(e => e.timestamp <= timestamp).ToList().Count > 0)
            {
                Event myEvent = eventList.Where(e => e.timestamp <= this.timestamp).First();
                HandleEvent(myEvent);

                eventList.Remove(myEvent);
            }

            //
            //remember queue size in timestamp

                Queue q = socket.queue;
                queueSize.Add(new QueueSize(timestamp, q, q.Count));

        }

        public void MakeEventStep()
        {
            if (eventList.Where(e => e.timestamp <= timestamp).ToList().Count > 0)
            {
                Event myEvent = eventList.Where(e => e.timestamp <= this.timestamp).First();
                HandleEvent(myEvent);
                eventList.Remove(myEvent);
            }
            else
            {
                //next step
                timestamp++;
                form.Notify("\n==" + this.timestamp + "==\n",1);
                JobGeneration();

                //
                //remember queue size in timestamp

                    Queue q = socket.queue;
                    queueSize.Add(new QueueSize(timestamp, q, q.Count));

            }
        }

        private void HandleEvent(Event myEvent)
        {
            if (myEvent is JobGenerationEvent)
            {
                //
                //tell
                String tell = "ZDARZENIE: wygrnerowanie zadania\n";

                JobGenerationEvent ev = (JobGenerationEvent)myEvent;
                Job job = ev.job;

                Socket s = socket;//List.GetFirstFreeSocket();

                bool added = false;
                if (s != null)  added = s.queue.Put(job);

                if (added)
                {
                    //
                    //tell
                    tell += "AKCJA: zad " + job.ToString() + " -> " + s.queue.ToString()+"\n ";
                }
                else
                {
                    jobList.Kill(killedJobsList, job );

                    //
                    //tell
                    tell += "AKCJA: zad " + job.ToString() + " zabite\n ";
                }

                eventList.Remove(myEvent);

                form.Notify(tell);
            }
            else if (myEvent is PutToQueueEvent)
            {
                //
                //tell
                String tell = "ZDARZENIE: zadanie w kolejce\n";

                PutToQueueEvent ev = (PutToQueueEvent)myEvent;
                Queue queue = ev.queue;
                Socket s = socket;//List.SingleOrDefault( s => s.queue == queue );

                //
                //dodaj zadanie na wolną maszynę
                
                //
                //sprawdz, czy wolna maszyna
                Device device = s.GetFirstFreeDevice();

                if (device != null && !queue.IsEmpty)
                {
                    Job job = queue.Get();
                    s.AddJobToFirstFreeDevice(job);
                    //
                    //tell
                    tell += "AKCJA: zad " + job.ToString() + " -> " + device.ToString() + "\n";
                }

                form.Notify(tell);
            }
            else if (myEvent is DeviceFinishedEvent)
            {
                
                //
                //tell
                String tell ="ZDARZENIE: maszyna ukończyła pracę\n";
                
                DeviceFinishedEvent ev = (DeviceFinishedEvent)myEvent;
                Device device = ev.device;
                Job job = ev.job;

                device.IsWorking = false;

                //
                //tell
                tell += "AKCJA: zad " + job.ToString() + " ukonczone na " + device.ToString()  + "\n";

                
                //
                //spróbój dodac zadanie do kolejki

                //pobierz socket maszyny
                Socket s = socket;//List.SingleOrDefault( x => x.deviceList.Contains(device));
                
                //pobierz nastepny socket
                
                //
                //jesli ostatni socket, wyrzuc
                //if (s.nextSockets == null || s.nextSockets.Count == 0)
                //{
                    job.Stop = this.timestamp;
                    device.RemoveJob(timestamp, s);

                    //
                    //to excel
                    //form.AddToExcelJobStatistic(((DeviceFinishedEvent)myEvent).job);


                    //
                    //tell
                    tell += "AKCJA: Zad " + job.ToString() + " ukonczone całkowicie\n";

                //}
                //
                // mamy nastepny socket, sprobój dodac zadanie do kolejki
                //else
                //{
                //    Socket nextSocket = socket.nextSockets.GetNextFreeSocket();

                //    if (nextSocket != null)
                //    {
                //        bool added = nextSocket.queue.Put(job);

                //        if (added)
                //        {
                //            device.RemoveJob(timestamp, socket);
                //            //
                //            //tell
                //            tell += "AKCJA: Zadanie dodane do kolejki: " + job.ToString() + ", " + nextSocket.queue.ToString() + "\n";
                //        }
                //    }
                //}

                form.Notify(tell);
            }
            else if (myEvent is DeviceEmptyEvent)
            {
                //
                //tell
                String tell = "ZDARZENIE: maszyna pusta\n";

                DeviceEmptyEvent ev = (DeviceEmptyEvent)myEvent;
                Socket socket = ev.socket;
                Queue q = socket.queue;

                if (!q.IsEmpty)
                {
                    Job job = q.Get();
                    socket.AddJobToFirstFreeDevice(job);
                }

                form.Notify(tell);

            }
            else if (myEvent is GetFromQueueEvent)
            {
                //
                //tell
                String tell = "ZDARZENIE: Pobranie zadnaie z kolejki\n";

                GetFromQueueEvent ev = (GetFromQueueEvent)myEvent;
                Queue queue = ev.queue;
                Socket s = socket;//List.SingleOrDefault(s => s.queue == queue);

                //
                //sprawdz, czy na poprzednich soketach nie ma maszyn z czekającymi zadaniami

                //List<Socket> prevSocketList = s.prevSockets;
                //Device prevBusyDev = prevSocketList.GetBusyDevice();
                

                ////
                ////wrzuć na kolejkę
                //if (prevBusyDev != null)
                //{
                //    Job job = prevBusyDev.CurrentJob;
                //    bool t = queue.Put(job);

                //    if (t)
                //    {
                //        prevBusyDev.RemoveJob(timestamp, socketList.GetSocketWithDevice(prevBusyDev));
                //    }

                //    //
                //    //tell
                //    tell += "AKCJA: zad" + job.ToString() + " -> " + queue.ToString() + "\n";
                //}

                form.Notify(tell);
            }
            else
            {
                throw new ApplicationException(" Can't handle event  ");
            }
        }

        internal void AddEvent(JobGenerationEvent e)
        {
            eventList.Add(e);
        }

        #region statistics

        //
        //devices statistics
        public double AvgBusyTimeOnDevice(Device dev)
        {
            int sum = 0;
            int count = 0;
            
            //get jobs on device
            foreach (Job job in jobList)
            {
                MachineTime mt = job.GetMachineTimeForDevice(dev);
                int start, stop;

                if (mt.start < 0) continue;
                else
                {
                    count++;
                    start = mt.start;
                }
                
                if (mt.stop > 0) stop = mt.stop;
                else stop = timestamp;
                int worktime = stop - start;
                sum += worktime;
            }
            if (count == 0) return 0;
            return (double)sum/count;
        }

        public double AvgWorkTimeOnDevice(Device dev)
        {
            int sum = 0;
            int count = 0;

            //get jobs on device
            foreach (Job job in jobList)
            {
                MachineTime mt = job.GetMachineTimeForDevice(dev);

                int start, stop;

                if (mt.start < 0) continue;
                else
                {
                    count++;
                    start = mt.start;
                }

                if (mt.stop > 0) stop = mt.stop;
                else stop = timestamp;
                int worktime = stop - start;
                if (worktime > mt.sec) worktime = mt.sec;
                sum += worktime;
            }
            if (count == 0) return 0;
            return (double)sum / count;
        }

        public int AllWorkTimeOnDevice(Device dev)
        {
            int time = 0;
            int count = 0;

            //get jobs on device
            foreach (Job job in jobList)
            {
                MachineTime mt = job.GetMachineTimeForDevice(dev);
                int start, stop;

                if (mt.start < 0) continue;
                else
                {
                    count++;
                    start = mt.start;
                }

                if (mt.stop > 0) stop = mt.stop;
                else stop = timestamp;
                int worktime = stop - start;
                if (worktime > mt.sec) worktime = mt.sec;
                time += worktime;
            }

            return time;
        }

        public int AllBusyTimeOnDevice(Device dev)
        {
            int time = 0;
            int count = 0;

            //get jobs on device
            foreach (Job job in jobList)
            {
                MachineTime mt = job.GetMachineTimeForDevice(dev);
                int start, stop;

                if (mt.start < 0) continue;
                else
                {
                    count++;
                    start = mt.start;
                }

                if (mt.stop > 0) stop = mt.stop;
                else stop = timestamp;

                int worktime = stop - start;

                time += worktime;

            }

            return time;
        }

        public int AllStartedJobsCount(Device dev)
        {
            return jobList
                .Where
                (
                    j =>
                    j.GetMachineTimeForDevice(dev).start >= 0
                )
                .Count();
        }

        public int AllStartedUJobsCount(Device dev)
        {

            return jobList
                .Where
                (
                    j => 
                    j.GetMachineTimeForDevice(dev).start >= 0
                )
                .Count();
        }

        public int AllStartedNJobsCount(Device dev)
        {
            return jobList
                .Where
                (
                    j =>
                    j.GetMachineTimeForDevice(dev).start >= 0
                )
                .Count();
        }

        //
        //queues statistics
        public double avgQueueTime(Queue queue)
        {
            double avg = 0;
            int count = 0;

            foreach (Job job in jobList)
            {
                QueueTime qt = job.GetQueueTimeForQueue(queue);
                int start, stop;

                if (qt.start > 0)
                {
                    start = qt.start;

                    if (qt.stop < 0)
                    {
                        stop = timestamp;
                    }
                    else
                    {
                        stop = qt.stop;
                    }

                    count ++;
                    avg += stop - start;
                }
            }

            if (count == 0) return 0;
            return avg /= count;
        }

        public int sumQueueTime(Queue queue)
        {
            int sum = 0;
            int count = 0;

            foreach (Job job in jobList)
            {
                QueueTime qt = job.GetQueueTimeForQueue(queue);
                int start, stop;

                if (qt.start > 0)
                {
                    start = qt.start;

                    if (qt.stop < 0)
                    {
                        stop = timestamp;
                    }
                    else
                    {
                        stop = qt.stop;
                    }

                    count++;
                    sum += stop - start;
                }
            }

            if (count == 0) return 0;
            return sum;
        }

        public int? maxQueueTime(Queue queue)
        {
            int? resut = null;
            foreach (Job job in jobList)
            {
                QueueTime qt = job.GetQueueTimeForQueue(queue);
                int start, stop;

                if (qt.start > 0)
                {
                    start = qt.start;

                    if (qt.stop > 0)
                    {
                        stop = qt.stop;

                        int time = stop - start;

                        if (resut != null)
                        {
                            resut = Math.Max(time, resut.Value);
                        }
                        else
                        {
                            resut = time;
                        }
                    }
                }
            }

            return resut;
        }

        //
        public int maxQueueCount(Queue queue)
        {
            List<QueueSize> queueSizes = queueSize.Where(x => x.Queue == queue).ToList();
            if (queueSizes.Count == 0) return 0;
            return queueSizes.Max(qs => qs.Size);
        }

        public double avgQueueCount(Queue queue)
        {
            List<QueueSize> queueSizes = queueSize.Where(x => x.Queue == queue).ToList();
            if (queueSizes.Count == 0) return 0;

            int sumQueueCount = 0;

            for (int i = 1; i < timestamp; ++i )
            {
                sumQueueCount += queueSizes.SingleOrDefault(qs => qs.Timestamp == i).Size;
            }

            return (double)sumQueueCount / timestamp;

        }

        //
        //job statistics
        public int MaxTimeInSystem()
        {
            List<Job> x = jobList.Where(j => j.Start >= 0 && j.Stop >= 0).ToList();
            if (x.Count <= 0) return 0;
            return    x.Max(j => j.TimeInSystem);     
        }

        public int MinTimeInSystem()
        {
            List<Job> x = jobList.Where(j => j.Start >= 0 && j.Stop >= 0).ToList();
            if (x.Count <= 0) return 0;
            return x.Min(j => j.TimeInSystem);   
        }

        public double AvgTimeInSystem()
        {
            List<Job> x = jobList.Where(j => j.IsFinished).ToList();
            if (x.Count <= 0) return 0;

            return x.Average(j => j.TimeInSystem); 
        }

        public int MaxWorkTime()
        {
            List<Job> x = jobList.Where(j => j.IsFinished).ToList();
            if (x.Count <= 0) return 0;
            return x.Max(j => j.WorkedTime());   
        }

        public int MinWorkTime()
        {
            List<Job> x = jobList.Where(j => j.IsFinished).ToList();
            if (x.Count <= 0) return 0;
            return x.Min(j => j.WorkedTime());
        }

        public double AvgWorkingTime()
        {
            List<Job> x = jobList.Where(j => j.Start >= 0 && j.Stop >= 0).ToList();

            int sum = 0;
            int count = 0;

            if (x.Count == 0) return 0;

            foreach (Job j in x)
            {
                sum += j.WorkedTime();
                count++;
            }

            return (double)sum / (double)count;
        }

        public double StdVarTimeInSystem()
        {
            List<Job> x = jobList.Where(j => j.IsFinished).ToList();
            if (x.Count <= 1) return 0;

            return x.Select(j => (double)j.TimeInSystem).CalculateStdDev();
        }

        public double StdVarWorkTime()
        {
            List<Job> x = jobList.Where(j => j.IsFinished).ToList();
            if (x.Count <= 1) return 0;

            return x.Select(j => (double)j.WorkedTime()).CalculateStdDev();
        }

        public int MaxWastedTime()
        {
            List<Job> x = jobList.Where(j => j.IsFinished).ToList();
            if (x.Count <= 0) return 0;
            return x.Max(j => j.WastedTime());     
        }

        public int MinWastedTime()
        {
            List<Job> x = jobList.Where(j => j.IsFinished).ToList();
            if (x.Count <= 0) return 0;
            return x.Min(j => j.WastedTime());
        }

        public double AvgWastedTime()
        {
            List<Job> x = jobList.Where(j => j.IsFinished).ToList();

            int sum = 0;
            int count = 0;

            if (x.Count == 0) return 0;

            foreach (Job j in x)
            {
                sum += j.WastedTime();
                count++;
            }

            return (double)sum / (double)count;
        }

        public double StdVarWastedTime()
        {
            List<Job> x = jobList.Where(j => j.IsFinished).ToList();
            if (x.Count <= 1) return 0;

            return x.Select(j => (double)j.WastedTime()).CalculateStdDev();
        }

        #endregion statistics


        
    }
}
