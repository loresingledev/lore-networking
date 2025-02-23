using Lore.Networking.Communication;

namespace Lore.Networking.Args
{
    public class MessageSentArgs : EventArgs
    {
        public readonly Message Message = null!;

        public MessageSentArgs(Message message)
        {
            this.Message = message;
        }
    }
}
