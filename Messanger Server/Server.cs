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
        public delegate void GetMessageDelegate(string message);
        event GetMessageDelegate GotMessage;
        int Port
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

        MessangerServer()
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
                        Thread.Sleep(1000);
                    } while (FirstClientSocket==null && SecongClientSocked ==null);
                    Console.WriteLine("Two clients have connected");
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
        private async void SendMessage(Socket TargetSocket)
        {
            await Task.Run(() =>
            {

            });
        }
    }
    class Server
    {
        static void Main(string[] args)
        {

        }
    }
}
