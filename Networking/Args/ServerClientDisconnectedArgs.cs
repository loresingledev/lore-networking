namespace Lore.Networking.Args
{
    public class ServerClientDisconnectedArgs : EventArgs
    {
        public readonly Connection Client = null!;

        public ServerClientDisconnectedArgs(Connection client)
        {
            Client = client;
        }
    }
}
