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
        private List<string> messageList;
        private Server()
        {

        }
        public Server(string address, int port) 
        {
            EndPoint = new IPEndPoint(IPAddress.Parse(address), port);
            messageList = new List<string>();
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
                    byte[] buffer = new byte[1024];
                    int i = handler.Receive(buffer);
                    messageList.Add(Encoding.Unicode.GetString(buffer, 0, i));


                    byte[] msg;
                    int l = messageList.Count;
                    do
                    {
                        msg = Encoding.Unicode.GetBytes(messageList[l - 1].ToCharArray());
                        handler.Send(msg);
                        l--;
                    } while (l > 0);



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
