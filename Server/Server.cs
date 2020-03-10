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
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                socket.Bind(EndPoint);
                socket.Listen(10);
                while (true)
                {
                    Console.WriteLine("ожидаем соединение");
                    Socket handler = socket.Accept();
                    Console.WriteLine("новое подключение: " + handler.RemoteEndPoint.ToString());
                    string data = null;
                    byte[] buffer = new byte[1024];
                    int i = handler.Receive(buffer);
                    data += Encoding.Unicode.GetString(buffer, 0, i);
                    Console.WriteLine("data: " + data + '\n');
                    string answer = "Спасибо за запрос";
                    byte[] msg = Encoding.Unicode.GetBytes(answer);
                    handler.Send(msg);


                    //do
                    //{
                    //    textReciveFromClient += Encoding.ASCII.GetString(buffer, 0, i);
                    //    Console.WriteLine(textReciveFromClient);
                    //} while (i > 0);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
