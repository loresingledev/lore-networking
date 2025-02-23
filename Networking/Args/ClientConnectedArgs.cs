using System.Net;

namespace Lore.Networking.Args
{
    public class ClientConnectedArgs : EventArgs
    {
        public readonly IPEndPoint RemotePoint = null!;

        public ClientConnectedArgs(IPEndPoint remotePoint)
        {
            RemotePoint = remotePoint;
        }
    }
}
