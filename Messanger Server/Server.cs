using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Messanger_Server
{
    class MessangerServer
    {
        bool IsWorking = false;
        int ServerPort = 0;
        Socket ServerSocket;
        Socket FirstClientSocket;
        Socket SecongClientSocked;
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
        async void Start()
        {
            await Task.Run(() =>
                {
                    IsWorking = true;
                    ServerSocket.Bind(new IPEndPoint(IPAddress.Any, ServerPort));
                    Console.WriteLine($"Server started on port {ServerPort}");
                    ServerSocket.Listen(2);

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
