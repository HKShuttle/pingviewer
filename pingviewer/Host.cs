using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using pingviewer.lib;
using System.Globalization;

namespace pingviewer
{
    internal class Host
    {
        private string hostname;
        private LongRingBuffer successRTT;
        private int count;
        private int success;
        private int fail;
        private Ping ping;

        public int Count { get; private set; }
        public int Success { get; private set; }
        public int Fail { get; private set; }
        public string Hostname { get; private set; }
        public LongRingBuffer SuccessRTT { get; private set; }

        public Host(string hostname)
        {
            this.hostname = hostname;
            ping = new Ping();
            count = 0;
            success = 0;
            fail = 0;
            successRTT = new LongRingBuffer(1000);
        }

        public PingReply SendPing()
        {
            return ping.Send(hostname);
        }
    }
}
