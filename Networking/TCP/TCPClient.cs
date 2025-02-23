using Lore.Networking.Args;
using Lore.Networking.Communication;
using System.Net;
using System.Net.Sockets;

namespace Lore.Networking.TCP
{
    public class TCPClient : Client
    {
        public override event EventHandler<ClientConnectedArgs> OnConnected = null!;

        public override event EventHandler OnDisconnected = null!;

        public override bool Connect(IPAddress address, uint port)
        {
            IPEndPoint remoteResource = new IPEndPoint(address, (int)port);

            socket.Connect(remoteResource);
            OnConnected?.Invoke(this, new ClientConnectedArgs(remoteResource));

            return true;
        }

        public override bool Disconnect()
        {
            connection.Close();
            OnDisconnected?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public override void Handle()
        {
            connection.Receive(out Message message);
        }

        public TCPClient() : base(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
            connection = new TCPConnection(socket);
        }
    }
}
