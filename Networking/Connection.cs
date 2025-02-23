using Lore.Networking.Args;
using Lore.Networking.Communication;
using System.Net;
using System.Net.Sockets;

namespace Lore.Networking
{
    public abstract class Connection
    {
        protected readonly Socket socket = null!;

        public IPEndPoint? RemotePoint => (IPEndPoint?)socket.RemoteEndPoint;

        public event EventHandler OnClosed = null!;

        public abstract event EventHandler<MessageSentArgs> OnSentMessage;

        public abstract event EventHandler<MessageReceivedArgs> OnReceivedMessage;

        public abstract bool IsWorking { get; }

        public abstract bool Send(Message message);

        public abstract bool Receive(out Message message);

        public bool Close()
        {
            if (IsWorking)
            {
                socket.Close();
                OnClosed?.Invoke(this, EventArgs.Empty);

                return true;
            }

            return false;
        }

        public Connection(Socket socket)
        {
            this.socket = socket;
        }
    }
}
