using System;
using System.Threading.Tasks;

namespace P2PChat
{ // State object for receiving data from remote device.

    public class Program
    {
        public static int Main(string[] args)
        {
            var asynchronousSocketListener = new AsynchronousSocketListener();

            var serverTask = new Task(() => asynchronousSocketListener.StartListening());

            serverTask.Start();

            var client = new Client();

            client.StartClient();

            Console.Read();
            return 1;
        }
    }
}