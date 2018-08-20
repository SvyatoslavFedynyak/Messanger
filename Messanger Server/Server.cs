using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Messanger_Server
{
    class MessangerServer
    {
        int MaxClients = 10;
        bool IsWorking = false;
        int ServerPort = 0;
        Socket[] SocketPull;
        Socket ServerSocket;
        public delegate void GetMessageDelegate(string message);
        event GetMessageDelegate GotMessage;
        public int Port
        {
            get { return ServerPort; }
            set
            {
                if (value > 0 && value < 25665)
                {
                    ServerPort = value;
                }
            }
        }

        public MessangerServer()
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public async void Start()
        {
            await Task.Run(() =>
                {
                    SocketPull = new Socket[MaxClients];
                    IsWorking = true;
                    GotMessage += Mailing;
                    ServerSocket.Bind(new IPEndPoint(IPAddress.Any, ServerPort));
                    Console.WriteLine($"Server started on port {ServerPort}");
                    ServerSocket.Listen(MaxClients);
                    ListenForConnections(ServerSocket);
                });
        }
        private async void ListenForConnections(Socket ListenSocket)
        {
            await Task.Run(() =>
            {
                do
                {
                    for (int i = 0; i < MaxClients; i++)
                    {
                        if (SocketPull[i] == null)
                        {
                            SocketPull[i] = ListenSocket.Accept();
                            Console.WriteLine("Client connected");
                            ListenForMessage(i);
                            break;
                        }
                    }
                } while (IsWorking);
            });
        }
        private async void ListenForMessage(int NumberInPull)
        {
            await Task.Run(() =>
            {
                byte[] buffer = new byte[1024];
                do
                {
                    SocketPull[NumberInPull].Receive(buffer);
                    GotMessage?.Invoke(Encoding.UTF8.GetString(buffer));

                } while (SocketPull[NumberInPull] != null);
            });

        }
        private async void Mailing(string message)
        {
            await Task.Run(() =>
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                foreach (Socket item in SocketPull)
                {
                    if (item != null)
                    {
                        item.Send(buffer);
                    }
                }
            });
        }
    }
    class Server
    {
        static void Main(string[] args)
        {
            MessangerServer server = new MessangerServer();
            server.Port = 904;
            server.Start();
            Console.ReadLine();
        }
    }
}
