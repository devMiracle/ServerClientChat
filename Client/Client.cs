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
        private Client()
        {
        }
        public Client(string ip, int port)
        {
            EndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }
        public string SendMessageToServer(string text)
        {
            string unswerFromServer = "";
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                if (socket.Connected)
                {
                    socket.Connect(EndPoint);
                    byte[] buffer = Encoding.ASCII.GetBytes(text);
                    socket.Send(buffer);
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
            return unswerFromServer;
        }
    }
}
