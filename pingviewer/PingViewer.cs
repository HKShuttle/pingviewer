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
        static void Main(string[] args)
        {
            Console.WriteLine("Ping to {0}, 1 block = 2ms", args[0]);
            Ping ping = new Ping();
            for (int i = 0; i < 1000; i++)
            {
                PingReply reply = ping.Send(args[0]);
                if(reply.Status == IPStatus.Success)
                {
                    var sb = new StringBuilder();
                    sb.Append("#");
                    for (int j = 0; j < reply.RoundtripTime; j += 2)
                    {
                        sb.Append("#");
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(sb.ToString());
                }
                else
                {   
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("X");
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
