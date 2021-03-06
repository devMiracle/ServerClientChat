﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace Client
{
    class Client
    {
        IPEndPoint EndPoint { get; set; }
        //IPHostEntry HostEntry { get; set; }
        private Client()
        {
        }
        public Client(string ip, int port)
        {
            //HostEntry = Dns.GetHostEntry(ip);
            EndPoint = new IPEndPoint(Dns.GetHostAddresses(ip)[0], port);
        }
        public string[] SendMessageToServer(string text)
        {
            List<string> list = new List<string>();
            byte[] buffer = new byte[1024];
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            if (text != "")
            {
                try
                {
                    socket.Connect(EndPoint);
                    if (socket.Connected)
                    {
                        int i;
                        byte[] msg;
                        i = 0;
                        msg = Encoding.Unicode.GetBytes(text);
                        socket.Send(msg);
                        do
                        {
                            i = socket.Receive(buffer);
                            list.Add(Encoding.Unicode.GetString(buffer, 0, i));
                        } while (i > 0);
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
                catch (SocketException e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
            else
            {
                MessageBox.Show("Введите сообщение");

            }
            
            return list.ToArray<string>();
        }
    }
}
