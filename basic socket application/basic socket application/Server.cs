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

// THIS IS SERVER -----------------------------------------------------------------------------------------------

namespace basic_socket_application
{
    class Server
    {
        static void Main(string[] args)
        {
            // Creating IPEndPoint which represents IP address, port pair.
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8000);
            // Than create socket, which will be used soon after.
            Socket newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Bind this socket to aforementioned IP endpoint (combination of IP address and port number).
            newSocket.Bind(localEndPoint);
            // And than listen on this socket. The integer in the brakets is: "The maximum length of the pending connections queue." 
            newSocket.Listen(10);
            // And now, accpet.
            newSocket.Accept();
            // The Socket object created by the Accept() method can now be used to transmit data in either direction between the server and the remote client.

        }
    }
}
