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
                    //получаем сообщение
                    byte[] buffer = new byte[1024];
                    int i = handler.Receive(buffer);
                    string text = Encoding.Unicode.GetString(buffer, 0, i);
                    
                    byte[] msg;
                    int l;

                    if (text == "/refresh")//пересылаем список клиенту
                    {
                        if (messageList.Count == 0)
                        {
                            handler.Send(Encoding.Unicode.GetBytes("список пуст"));
                        }
                        else
                        {
                            l = messageList.Count;
                            do
                            {
                                msg = Encoding.Unicode.GetBytes(messageList[l - 1].ToString());
                                handler.Send(msg);
                                l--;
                            } while (l > 0);
                        }
                    }
                    else//добавляем сообщение в лист и пересылаем список клиенту
                    {
                        //if (i > 0)
                        //{
                            messageList.Add(text);
                            l = messageList.Count;
                            do
                            {
                                msg = Encoding.Unicode.GetBytes(messageList[l - 1].ToString());
                                handler.Send(msg);
                                l--;
                            } while (l > 0);
                        //}
                        //else
                        //{
                            //handler.Send(Encoding.Unicode.GetBytes("введите сообщение"));
                        //}
                        
                    }

                    



                    Console.WriteLine("отключился: " + handler.RemoteEndPoint.ToString());
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
