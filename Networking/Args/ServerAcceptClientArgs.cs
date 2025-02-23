namespace Lore.Networking.Args
{
    public class ServerAcceptClientArgs : EventArgs
    {
        public readonly Connection Client = null!;

        public ServerAcceptClientArgs(Connection client)
        {
            Client = client;
        }
    }
}
