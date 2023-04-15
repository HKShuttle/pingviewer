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
        private long lastRTT;
        private Ping ping;

        public Host(string hostname)
        {
            this.hostname = hostname;
            ping = new Ping();
            count = 0;
            success = 0;
            fail = 0;
            successRTT = new LongRingBuffer(1000);
        }

        public IPStatus CheckReachability()
        {
            PingReply reply = ping.Send(hostname);
            count++;

            if (reply.Status == IPStatus.Success) 
            {
                success++;
                lastRTT = reply.RoundtripTime;
                successRTT.enqueue(lastRTT);
                // MainメソッドからRTTの取得はLastRTTアクセサを通して行う
            }
            else
            {
                fail++;
            }

            return reply.Status;
        }
    }
}
