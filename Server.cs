using PhemoteDesktop.Properties;
using System.Net;
using System.Net.Sockets;

namespace PhemoteDesktop
{
    public class Server
    {
        public IPAddress LocalIPAddress = IPAddress.Parse(Resources.IP);
        private TcpListener listener;

        public Server()
        {
            int port = int.Parse(Resources.Port);
            listener = new TcpListener(LocalIPAddress, port);
            listener.Start();
            Thread thread = new(new ThreadStart(ListenForClients))
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void ListenForClients()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Thread clientThread = new(new ThreadStart(() => HandleClient(client)))
                {
                    IsBackground = true
                };
                clientThread.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            client.Close();
        }
    }
}
