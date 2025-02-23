namespace Lore.Networking.Args
{
    public class ServerShutdownArgs : EventArgs
    {
        public readonly DateTime ShutdownedAt;

        public ServerShutdownArgs()
        {
            this.ShutdownedAt = DateTime.Now;
        }
    }
}
