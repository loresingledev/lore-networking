using System.Net.Sockets;
using System.Net;
using Lore.Networking.Args;

namespace Lore.Networking
{
    public abstract class Client
    {
        protected readonly Socket socket = null!;

        protected Connection connection = null!;

        public Connection Connection => connection;

        public abstract event EventHandler<ClientConnectedArgs> OnConnected;

        public abstract event EventHandler OnDisconnected;

        public abstract bool Connect(IPAddress address, uint port);

        public abstract bool Disconnect();

        public abstract void Handle();

        protected Client(Socket socket)
        {
            this.socket = socket;
        }
    }
}
