using System;
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
        public string SendMessageToServer(string text)
        {
            byte[] buffer = new byte[1024];
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            int l = 0;
            try
            {
                socket.Connect(EndPoint);
                if (socket.Connected)
                {
                    byte[] msg = Encoding.Unicode.GetBytes(text);
                    int i = socket.Send(msg);
                    l = socket.Receive(buffer);

                    //int i;
                    //do
                    //{
                    //    i = socket.Receive(buffer);
                    //    unswerFromServer += Encoding.ASCII.GetString(buffer, 0, 1);
                    //} while (i > 0);
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
            return Encoding.Unicode.GetString(buffer, 0, l);
        }
    }
}
