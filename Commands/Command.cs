using System.Diagnostics;
using Concept2.Enums;

namespace Concept2.Commands
{
    public abstract class Command
    {
        abstract protected void ReadInternal(ResponseReader reader);

        protected Command(CSAFE id, uint rspSize)
        {
            m_Id = id;
            m_RspSize = rspSize;
        }

        public void Write(CommandWriter writer)
        {
            writer.WriteByte((uint)m_Id);
        }

        public void Read(ResponseReader reader)
        {
            uint id = reader.ReadByte();
            uint size = reader.ReadByte();

            if (id == (uint)m_Id && size == m_RspSize)
                ReadInternal(reader);
            else
                Debug.WriteLine("[Command.Read] id/size mismatch");
        }

        private CSAFE m_Id;
        private readonly uint m_RspSize;
    }
}
