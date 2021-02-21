using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        public static string data = null;

        public static void StartListening()
        {
            byte[] bytes = new byte[1024];

            IPAddress ipAddress = IPAddress.Loopback;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 8080);
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);
                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();

                while (true)
                {
                    
                    data = null;

                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        int eofIndex = data.IndexOf("<EOF>");
                        if ( eofIndex > -1)
                        {
                            Console.WriteLine("Text received : {0}", data.Remove(eofIndex));
                            break;
                        }
                    }


                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    handler.Send(msg);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static void Main(String[] args)
        {
            StartListening();
        }
    }
}
