using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        
        static void StartClient()
        {
            byte[] bytes = new byte[1024];

            try
            {
                IPAddress iPAddress = IPAddress.Loopback;
                IPEndPoint endPoint = new IPEndPoint(iPAddress, 8080);
                Socket client = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(endPoint);
                Console.WriteLine("Connected to {0}", iPAddress);
                while (true)
                {
                    string msg = Console.ReadLine();
                    byte[] byteMsg = Encoding.ASCII.GetBytes($"{msg}<EOF>");
                    int bytesCount = client.Send(byteMsg);
                    int receivedCount = client.Receive(bytes);
                    Console.WriteLine("Text received = {0}", Encoding.ASCII.GetString(bytes, 0, receivedCount-5));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        static void Main(string[] args)
        {
            StartClient();
        }
    }
}
