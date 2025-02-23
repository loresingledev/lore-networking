using System.Net.Sockets;

namespace Lore.Networking.Args
{
    public class ConnectionSocketBrokenArgs : EventArgs
    {
        public readonly SocketError Error = SocketError.SocketError;

        public ConnectionSocketBrokenArgs(SocketError error)
        {
            this.Error = error;
        }
    }
}
