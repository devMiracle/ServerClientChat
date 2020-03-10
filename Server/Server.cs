using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class Server
    {
        public IPEndPoint EndPoint { get; set; }

        private Server()
        {

        }
        public Server(string address, int port) 
        {
            EndPoint = new IPEndPoint(IPAddress.Parse(address), port);
        }

        public void Start()
        {
            try
            {
                string textReciveFromClient = "";
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                socket.Bind(EndPoint);
                socket.Listen(10);
                while (true)
                {
                    Socket socketClient = socket.Accept();
                    byte[] buffer = new byte[1024];
                    int i = socketClient.Receive(buffer);
                    do
                    {
                        textReciveFromClient += Encoding.ASCII.GetString(buffer, 0, i);
                        Console.WriteLine(textReciveFromClient);
                    } while (i > 0);
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                
            }



        }

    }
}
