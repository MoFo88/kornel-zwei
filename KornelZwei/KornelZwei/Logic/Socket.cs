using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KornelZwei.Logic
{
    public class Socket
    {
        public static int lastId = 0;

        //place
        public int X { get; set; }
        public int Y { get; set; }

        public Scheduler scheduler;

        public bool IsFirst { get; set; }
        public List<Device> deviceList;
        public List<Socket> prevSockets;
        public List<Socket> nextSockets;
        public int Probability { get; set; }

        public int Id  {get; set;}

        public Queue queue;

        public Socket(Queue q, Scheduler s,  bool isFirst = false)
        {
            ++lastId;

            Id = lastId;

            this.IsFirst = isFirst;
            queue = q;
            deviceList = new List<Device>();
            prevSockets = new List<Socket>();
            nextSockets = new List<Socket>();
            scheduler = s;
        }

        public void AddDevice(Device dev)
        {
            deviceList.Add(dev);
            dev.socket = this;
        }

        public static void MakeConnection(Socket prev, Socket next, int probability = 100)
        {
            if (prev == next) return;

            if (!prev.nextSockets.Contains(next))
            {
                prev.nextSockets.Add(next);
                next.Probability = probability;
            }

            if (!next.prevSockets.Contains(prev))
            {
                next.prevSockets.Add(prev);
            }
        }

        public  List<Device> GetFreeDevices()
        {
            return deviceList.Where(d => d.IsBusy == false).ToList();
        }

        public  Device GetFirstFreeDevice()
        {
            List<Device> list =  deviceList.Where(d => d.IsBusy == false).ToList();

            if (list.Count == 0) return null;
            return list.First();
        }

        public  Device AddJobToFirstFreeDevice(Job job)
        {
            //pierwsza wolna maszyna
            Device firstfreeDevice = this.GetFirstFreeDevice();

            if (firstfreeDevice != null)
            {
                //event: zadanie ukonczy sie w chwili t
                AddEventDeviceFinished(firstfreeDevice, job);

                firstfreeDevice.CurrentJob = job;
                return firstfreeDevice;
            }
            else
            {
                return null;
            }
        }

        public void AddEventDeviceFinished(Device d , Job job)
        {


            DeviceFinishedEvent ev = new DeviceFinishedEvent(d, job, scheduler.timestamp);
            scheduler.AddEvent(ev);
        }

        public override string ToString()
        {
            return "s" + Id;
        }
    }
    
}
