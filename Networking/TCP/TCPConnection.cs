using Lore.Networking.Args;
using Lore.Networking.Communication;
using System.Net.Sockets;

namespace Lore.Networking.TCP
{
    public class TCPConnection : Connection
    {
        public override bool IsWorking => !(socket.Poll(0, SelectMode.SelectRead) && socket.Available == 0);

        public override event EventHandler<MessageSentArgs> OnSentMessage = null!;

        public override event EventHandler<MessageReceivedArgs> OnReceivedMessage = null!;

        public override bool Send(Message message)
        {
            try
            {
                if (!IsWorking)
                    return false;

                int amount = socket.Send(message.Bytes, message.Length, SocketFlags.None);
                OnSentMessage?.Invoke(this, new MessageSentArgs(message));

                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public override bool Receive(out Message message)
        {
            message = null!;

            try
            {
                if (socket.Available > 0 && IsWorking)
                {
                    byte[] buffer = new byte[socket.ReceiveBufferSize];
                    int amount = socket.Receive(buffer);

                    message = new Message(buffer, amount);

                    OnReceivedMessage?.Invoke(this, new MessageReceivedArgs(message));
                    return true;
                }

                return false;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public TCPConnection(Socket socket) : base(socket)
        {

        }
    }
}
