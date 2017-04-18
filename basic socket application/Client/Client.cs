using System;
using System.Net;
using System.Net.Sockets;

/**** CONNECTION oriented scheme ****

        SERVER      CLIENT
        create endpoint in both programs, than:
        socket()    socket()
        bind()
        listen()
        accept() <- connect()
        recv() <--- send()
        send() ---> recv()
        close() <-> close
*/

// THIS IS CLIENT -----------------------------------------------------------------------------------------------

namespace basic_socket_application
{
    class Client
    {
        static void Main(string[] args)
        {
            // Creating IPEndPoint which represents IP address, port pair.
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            // Than create socket.
            Socket toServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Than connect socket to IP address and port. (IP End point)
            toServer.Connect(clientEndPoint);
        }
    }
}
