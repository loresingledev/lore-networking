using Lore.Networking.Args;
using Lore.Networking.Communication;
using System.Net;
using System.Net.Sockets;

namespace Lore.Networking.TCP
{
    public class TCPServer : Server
    {
        public override event EventHandler OnStarted = null!;

        public override event EventHandler OnShutdowned = null!;

        public override event EventHandler<ServerAcceptClientArgs> OnClientAccepted = null!;

        public override event EventHandler<ServerClientDisconnectedArgs> OnClientDisconnected = null!;

        public override event EventHandler<MessageReceivedArgs> OnClientMessageReceived = null!;

        public override bool Start(IPAddress address, uint port)
        {
            socket.Bind(new IPEndPoint(address, (int)port));
            socket.Listen();

            OnStarted?.Invoke(this, EventArgs.Empty);

            clients = new Dictionary<IPEndPoint, Connection>();
            return true;
        }

        public override bool Shutdown(Message reason)
        {
            Announce(reason);

            clients = null!;
            socket.Close();

            OnShutdowned?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public override bool Accept(out Connection connection)
        {
            if (socket.Poll(0, SelectMode.SelectRead))
            {
                Socket clientSocket = socket.Accept();
                connection = new TCPConnection(clientSocket);

                clients.Add(connection.RemotePoint!, connection);
                OnClientAccepted?.Invoke(this, new ServerAcceptClientArgs(connection));

                return true;
            }

            connection = null!;
            return false;
        }

        public override void Handle()
        {
            Accept(out Connection _);

            foreach (Connection connection in clients.Values)
            {
                if (!connection.IsWorking)
                {
                    clients.Remove(connection.RemotePoint!);
                    OnClientDisconnected?.Invoke(this, new ServerClientDisconnectedArgs(connection));

                    connection.Close();
                    continue;
                }

                if (connection.Receive(out Message message))
                {
                    OnClientMessageReceived?.Invoke(connection, new MessageReceivedArgs(message));
                }
            }
        }

        public TCPServer() : base(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {

        }
    }
}
