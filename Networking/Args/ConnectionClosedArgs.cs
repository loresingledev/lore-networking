namespace Lore.Networking.Args
{
    public class ConnectionClosedArgs : EventArgs
    {
        public readonly Connection Connection = null!;

        public ConnectionClosedArgs(Connection connection)
        {
            this.Connection = connection;
        }
    }
}
