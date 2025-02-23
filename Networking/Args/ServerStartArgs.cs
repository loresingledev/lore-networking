namespace Lore.Networking.Args
{
    public class ServerStartArgs : EventArgs
    {
        public readonly DateTime StartedAt;

        public ServerStartArgs()
        {
            this.StartedAt = DateTime.Now;
        }
    }
}
