using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        private static string ip = "127.0.0.1";
        private static int port = 1024;
        static void Main(string[] args)
        {
            Console.Title = ip + ":" + port;
            Console.WindowWidth = 40;
            Console.WindowHeight = 20;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

            Server server = new Server(ip, port);
            server.Start();
        }
    }
}
