using Lore.Networking.Communication;

namespace Lore.Networking.Args
{
    public class MessageReceivedArgs : EventArgs
    {
        public readonly Message Message = null!;

        public MessageReceivedArgs(Message message)
        {
            this.Message = message;
        }
    }
}
