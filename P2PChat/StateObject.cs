using System.Net.Sockets;
using System.Text;

namespace P2PChat
{
    public class StateObject
    {
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] Buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder Sb = new StringBuilder();
        // Client socket.
        public Socket WorkSocket;
    }
}