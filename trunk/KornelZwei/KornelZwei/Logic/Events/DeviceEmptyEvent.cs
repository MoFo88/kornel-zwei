using System;
using System.Collections.Generic;
using System.Text;

namespace KornelZwei.Logic
{
    public class DeviceEmptyEvent : Event
    {
        public Socket socket;
        public DeviceEmptyEvent(Socket soc, int t)
            : base(t)
        {
            socket = soc;
        }
    }
}
