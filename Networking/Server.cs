using Lore.Networking.Args;
using Lore.Networking.Communication;
using System.Net;
using System.Net.Sockets;

namespace Lore.Networking
{
    public abstract class Server
    {
        protected readonly Socket socket = null!;

        protected Dictionary<IPEndPoint, Connection> clients = null!;

        public abstract event EventHandler OnStarted;

        public abstract event EventHandler OnShutdowned;

        public abstract event EventHandler<ServerAcceptClientArgs> OnClientAccepted;

        public abstract event EventHandler<ServerClientDisconnectedArgs> OnClientDisconnected;

        public abstract event EventHandler<MessageReceivedArgs> OnClientMessageReceived;

        public abstract bool Start(IPAddress address, uint port);

        public abstract bool Shutdown(Message reason);

        public abstract bool Accept(out Connection connection);

        public abstract void Handle();

        public void Announce(Message message, List<IPEndPoint> exclude = null!)
        {
            foreach (Connection connection in clients.Values)
            {
                if (exclude != null && exclude.Contains(connection.RemotePoint!))
                    continue;

                connection.Send(message);
            }
        }

        protected Server(Socket socket)
        {
            this.socket = socket;
        }
    }
}
