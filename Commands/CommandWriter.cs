using Concept2.Exceptions;

namespace Concept2.Commands
{
    public class CommandWriter
    {
        public CommandWriter(int capacity)
        {
            m_Buffer = new uint[capacity];
            m_Size = 0;
        }

        public int Size => m_Size;
        public uint[] Buffer => m_Buffer;

        public void Reset()
        {
            m_Size = 0;
        }

        public void WriteByte(uint val)
        {
            if (m_Size >= m_Buffer.Length)
                throw new BufferExceededException("Attempted to write past end of command buffer.");

            m_Buffer[m_Size++] = val;
        }

        private readonly uint[] m_Buffer;
        private int m_Size;
    }
}
