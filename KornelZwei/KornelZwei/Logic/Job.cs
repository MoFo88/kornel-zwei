using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Kolejki.MyMath;
using System.Drawing;
using KornelZwei.Logic.MyMath;


namespace KornelZwei.Logic
{
    public class Job
    {
        private static int ID = 0;
        
        public List<MachineTime> DeviceTimeList {get; set;}
        public List<QueueTime> QueueTimeList { get; set; }
        public List<Device> VisitedDevices { get; set; }

        public int Id { get; set; }
        public String Name { get; set; }
        public String ToStr { get { return Id + "[" + Start + "], " ; } }
        public int Start {get; set;}
        public int Stop { get; set; }
        //public IDistribution Distribution { get; set; }
        public bool IsFinished { get { if (Stop > 0) return true; return false; } }
        public int TimeInSystem { get { return Stop - Start; } }
        public Color color;

        public override string ToString()
        {
            return  ToStr;
        }

        public int WorkedTime()
        {
            int sum = 0;

            foreach (Device dev in VisitedDevices)
            {
                MachineTime mt = GetMachineTimeForDevice(dev);
                sum += mt.sec;
            }

            return sum;
        }

        public int WastedTime()
        {
            return TimeInSystem - WorkedTime();
        }

        public double AvgWorkTime()
        {
            int count = VisitedDevices.Count();
            int sum = WorkedTime();

            if (count == 0) return 0;
            return (double)sum / (double)count;
            
        }

        public int  MaxWorkTime()
        {
            int max = -1;
            foreach (Device dev in VisitedDevices)
            {
                MachineTime mt = GetMachineTimeForDevice(dev);
                max = Math.Max(max, mt.sec);
            }
            return max;
        }

        public int MinWorkTime()
        {
            if (VisitedDevices.Count == 0) return 0;
            int min = GetMachineTimeForDevice(VisitedDevices[0]).sec;
            foreach (Device dev in VisitedDevices)
            {
                MachineTime mt = GetMachineTimeForDevice(dev);
                min = Math.Min(min, mt.sec);
            }
            return min;
        }

        public double StdVarWorkTime()
        {
            List<double> result = VisitedDevices.Select(v => (double)GetMachineTimeForDevice(v).sec).ToList();
            if (result.Count <= 1) return 0;
            return result.CalculateStdDev();
        }

        public MachineTime GetMachineTimeForDevice(Device device)
        {
            return DeviceTimeList.SingleOrDefault(t => t.device == device);
        }

        public QueueTime GetQueueTimeForQueue(Queue queue)
        {
            return QueueTimeList.SingleOrDefault(t => t.queue == queue);
        }

        internal void AddVisitedDevice(Device device)
        {
            if (!VisitedDevices.Contains(device)) VisitedDevices.Add(device);
        }
        /// <summary>
        /// constructor - initialize job
        /// </summary>
        /// <param name="globalSocketList"></param>
        /// <param name="distr"></param>
        public Job(Socket socket, int time)
        {
            Job.ID++;
            Id = Job.ID;
            Name = "Zadanie " + Id;

            //
            //lista czasów wykonywania zdania na maszynach
            DeviceTimeList = new List<MachineTime>();
            QueueTimeList = new List<QueueTime>();
            VisitedDevices = new List<Device>();

            color = Color.FromArgb(((Id+10) * 20) % 255, (Id * 30) % 255, (Id * 40) % 255);

            //foreach (Socket socket in globalSocketList)
            //{

            //losowanieczasu tankowania
                //initialize device time list
                foreach (Device device in socket.deviceList)
                {
                    MachineTime mt = new MachineTime();
                    mt.device = device;
                    mt.sec = 20;

                    DeviceTimeList.Add(mt);
                }

                //initialize queue time list
                Queue queue = socket.queue;

                QueueTime qt = new QueueTime();
                qt.queue = queue;

                QueueTimeList.Add(qt);
            //}

            Start = time;
            Stop = -1;  
        }
    }
}
