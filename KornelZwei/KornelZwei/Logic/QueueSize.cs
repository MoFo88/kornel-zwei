using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KornelZwei.Logic
{
    public class QueueSize
    {
        public int Timestamp{get; set;}
        public Queue Queue { get; set; }
        public int Size { get; set; }
        public QueueSize(int timestamp, Queue queue, int size) { Timestamp = timestamp; Queue = queue; Size = size; }
    }
}
