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
        bool IsWorking = false;
        int ServerPort = 0;
        Socket ServerSocket;
        Socket FirstClientSocket;
        Socket SecongClientSocked;
        string FirstClientMessage;
        string SecondClientMessage;
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
                    IsWorking = true;
                    ServerSocket.Bind(new IPEndPoint(IPAddress.Any, ServerPort));
                    Console.WriteLine($"Server started on port {ServerPort}");
                    ServerSocket.Listen(2);
                    FirstClientSocket = GetConnect(ServerSocket).Result;
                    SecongClientSocked = GetConnect(ServerSocket).Result;
                    do
                    {
                        Thread.Sleep(100);
                    } while (FirstClientSocket == null && SecongClientSocked == null);
                    Console.WriteLine("Two clients have connected");
                    WaitForMessage1(FirstClientSocket);
                    WaitForMessage2(SecongClientSocked);
                });
        }
        private async Task<Socket> GetConnect(Socket ListenSocket)
        {
            return await Connect(ListenSocket);
        }
        private Task<Socket> Connect(Socket ListenSocket)
        {
            return Task.Run(() =>
                       {
                           return ListenSocket.Accept();
                       });
        }
        private async void WaitForMessage1(Socket TargetSocket)
        {
            await Task.Run(() =>
            {
                do
                {
                    byte[] buffer = new byte[1024];
                    TargetSocket.Receive(buffer);
                    Console.WriteLine(Encoding.ASCII.GetString(buffer));
                    SecongClientSocked.Send(buffer);
                } while (IsWorking);
            });
        }
        private async void WaitForMessage2(Socket TargetSocket)
        {
            await Task.Run(() =>
            {
                do
                {
                    byte[] buffer = new byte[1024];
                    TargetSocket.Receive(buffer);
                    Console.WriteLine(Encoding.ASCII.GetString(buffer));
                    FirstClientSocket.Send(buffer);
                } while (IsWorking);
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
