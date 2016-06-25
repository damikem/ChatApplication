using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var userName = Console.ReadLine();
            new Chat(userName);
        }
    }

    public class Chat
    {
        private const int Port = 54545;
        private const string BroadcastAddress = "255.255.255.255";
        private readonly string _userName;
        private UdpClient _receivingClient;
        private Thread _receivingThread;
        private UdpClient _sendingClient;

        public Chat(string userName)
        {
            _userName = userName;

            InitializeSender();

            InitializeReceiver();

            while (true)
            {
                var message = Console.ReadLine();
                SendMessage(message);
            }
        }

        private void InitializeSender()
        {
            _sendingClient = new UdpClient(BroadcastAddress, Port) {EnableBroadcast = true};
        }

        private void InitializeReceiver()
        {
            _receivingClient = new UdpClient(Port);
            
            ThreadStart start = Receiver;

            _receivingThread = new Thread(start) {IsBackground = true};
            
            _receivingThread.Start();
        }

        private void SendMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var toSend = _userName + ":\n" + message;

                var data = Encoding.ASCII.GetBytes(toSend);

                _sendingClient.Send(data, data.Length);
            }
        }

        private void Receiver()
        {
            var endPoint = new IPEndPoint(IPAddress.Any, Port);

            AddMessage messageDelegate = MessageReceived;

            while (true)
            {
                var data = _receivingClient.Receive(ref endPoint);

                var message = Encoding.ASCII.GetString(data);

                messageDelegate(message);
            }
        }

        private void MessageReceived(string message)
        {
            Console.WriteLine(message);
        }

        private delegate void AddMessage(string message);
    }
}