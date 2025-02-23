using System.Text;

namespace Lore.Networking.Communication
{
    public class Message
    {
        public static Encoding Encoding => Encoding.UTF8;

        public readonly byte[] Bytes = null!;

        public readonly int Length = 0;

        public override string ToString()
        {
            return Encoding.GetString(Bytes, 0, Length);
        }

        public Message(byte[] bytes)
        {
            this.Bytes = bytes;
            this.Length = bytes.Length;
        }

        public Message(byte[] bytes, int length)
        {
            this.Bytes = bytes;
            this.Length = length;
        }

        public Message(string text)
        {
            this.Bytes = Encoding.GetBytes(text);
            this.Length = Bytes.Length;
        }
    }
}
