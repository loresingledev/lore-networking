namespace Lore.Networking.Args
{
    public class ServerAcceptionArgs : EventArgs
    {
        public readonly Connection Connection;

        public readonly DateTime AcceptedAt;

        public ServerAcceptionArgs(Connection connection, DateTime acceptedAt)
        {
            this.Connection = connection;
            this.AcceptedAt = acceptedAt;
        }
    }
}
