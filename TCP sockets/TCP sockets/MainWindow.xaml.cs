using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace TCP_sockets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btn_send.IsEnabled = false;
        }

        private int LengthofTheUsefulStream(byte[] field)
        {
            int numOfNonNulls = 0;
            for (int x = 0; x < field.Length; x++)
            {
                if (field[x] != 0)
                {
                    numOfNonNulls++;
                }
            }

            numOfNonNulls *= 2;

            if (numOfNonNulls > 1024)
                numOfNonNulls = 1024;

            return numOfNonNulls;
        }

        private void HandleTheClient(object object_client)
        {
            try
            {
                TcpClient client = (TcpClient)object_client;
                NetworkStream stream = client.GetStream(); // Network stream: Provides the underlying stream of data for network access ; .GetStream(): Returns the NetworkStream used to send and receive data.

                byte[] buffer = new byte[1024];
                stream.Read(buffer, 0, buffer.Length); // An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.
                int usedBufferLenght = LengthofTheUsefulStream(buffer);
                byte[] truncatedBuffer = new byte[usedBufferLenght];

                for (int i = 0; i < usedBufferLenght; i++)
                {
                    truncatedBuffer[i] = buffer[i];
                }

                tb_messages.Dispatcher.Invoke(() => tb_messages.Text += "\nnew message: " + Encoding.Unicode.GetString(truncatedBuffer)); // GetString: When overridden in a derived class, decodes all the bytes in the specified byte array into a string.
                stream.Close(); //Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream. Instead of calling this method, ensure that the stream is properly disposed.
                client.Close(); // Disposes this TcpClient instance and requests that the underlying TCP connection be closed.
            }
            catch
            {
                tb_messages.Dispatcher.Invoke(() => tb_messages.Text = "You screwed up, ereasing your system partition...");
            }
        }

        private void RunServer()
        {
            try
            {
                string helpingVariable_listeningPort = tb_messages.Dispatcher.Invoke(() => { return (tb_listeningPort.Text); }); // Anonymous function returns value from thread text box listening port runs in.
                int listeningPort = int.Parse(helpingVariable_listeningPort);

                TcpListener tcpListener = new TcpListener(IPAddress.Any, listeningPort); // listen on IP endpoint (IP endpoint is a pair of IP address and a port number.)
                tcpListener.Start(); // a nastartovat TCP listener

                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient(); // Accepts a pending connection request. TCP client: Provides client connections for TCP network services.
                    // HandleTheClient(client);
                    Thread threadwithParams_HandletheCLient = new Thread(new ParameterizedThreadStart(HandleTheClient));
                    threadwithParams_HandletheCLient.Start(client);
                }

            }
            catch (Exception e)
            {
                tb_messages.Dispatcher.Invoke(() => tb_messages.Text += "\n\nAn error occured:\n " + e.ToString()); // za to co co je uz v messages pridaj error ktory dostanes vo vynimke
            }
            
            
        }

        private void RunClient()
        {
            try
            {
                string serversIPAddress = tb_serversIPAddress.Dispatcher.Invoke(() => { return tb_serversIPAddress.Text; }); // Anonymna funkcia na vracanie textu pre broadcasting port text box.
                serversIPAddress = serversIPAddress.ToString(); 

                string helpingVariable_broadcastingPort = tb_broadcastingPort.Dispatcher.Invoke(() => { return tb_broadcastingPort.Text; }); // Anonymna funkcia na vracanie textu pre broadcasting port text box.
                int broadcastingPort = int.Parse(helpingVariable_broadcastingPort);

                TcpClient client = new TcpClient(); // create client object
                client.Connect(serversIPAddress, broadcastingPort); // Connects the client to a remote TCP host using the specified IP address and port number.

                NetworkStream stream = client.GetStream(); // Network stream: Provides the underlying stream of data for network access ; .GetStream(): Returns the NetworkStream used to send and receive data.
                string s = tb_textToSend.Dispatcher.Invoke(() => { return tb_textToSend.Text; });
                byte[] message = Encoding.Unicode.GetBytes(s);
                stream.Write(message, 0, message.Length); // When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
                /* Close the connection because the port needs to be free for another connection. */
                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                tb_messages.Dispatcher.Invoke(() => tb_messages.Text = "You screwed up, ereasing your system partition...");

                tb_messages.Dispatcher.Invoke(() => tb_messages.Text += "\n\nAn error occured:\n " + e.ToString()); // za to co co je uz v messages pridaj error ktory dostanes vo vynimke
            }
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            btn_start.IsEnabled = false;
            Thread thread_RunServer = new Thread(RunServer); // New thread for server.
            thread_RunServer.Start();
            btn_send.IsEnabled = true;
        }

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            Thread thread_RunClient = new Thread(RunClient); // New thread for client.
            thread_RunClient.Start();
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
