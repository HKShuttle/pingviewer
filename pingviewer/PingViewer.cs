using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace pingviewer
{
    internal class PingViewer
    {
        static int count = 0;
        static int success = 0;
        static int fail = 0;
        static bool loop = true;
        static long min = long.MaxValue;
        static long max = long.MinValue;

        static void Main(string[] args)
        {
            // Establish an event handler to process key press events.
            Console.CancelKeyPress += new ConsoleCancelEventHandler(interruptHandler);

            Console.WriteLine("Ping to {0}, 1 block = 2ms", args[0]);
            Ping ping = new Ping();
            while (count < 1000 && loop)
            {
                PingReply reply = ping.Send(args[0]);
                count++;
                if (reply.Status == IPStatus.Success)
                {
                    success++;
                    if (reply.RoundtripTime < min)
                    {
                        min = reply.RoundtripTime;
                    }
                    if (reply.RoundtripTime > max)
                    {
                        max = reply.RoundtripTime;
                    }

                    var sb = new StringBuilder();
                    for (int i = 0; i < reply.RoundtripTime && i <= 100; i += 2)
                    {
                        sb.Append("#");
                    }
                    if (reply.RoundtripTime == 0)
                    {
                        sb.Append("-");
                    }
                    if (reply.RoundtripTime > 100)
                    {
                        sb.Append("+");
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(sb.ToString());
                }
                else
                {
                    fail++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("X");
                }
                System.Threading.Thread.Sleep(1000);
            }
            summary();
        }

        protected static void interruptHandler(object sender, ConsoleCancelEventArgs e)
        {
            loop = false;
            e.Cancel = true;
        }

        protected static void summary()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("----------");
            Console.WriteLine("ICMP echo requests: {0}", count);
            Console.WriteLine("Success replys: {0}", success);
            Console.WriteLine("Fail replys: {0}", fail);
            if (success > 0)
            {
                Console.WriteLine("Minimum RTT: {0}ms", min);
                Console.WriteLine("Maximum RTT: {0}ms", max);
            }
            else
            {
                Console.WriteLine("Minimum RTT: --ms");
                Console.WriteLine("Maximum RTT: --ms");
            }
        }

    }
}
