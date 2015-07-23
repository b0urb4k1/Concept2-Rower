using System.Diagnostics;

namespace Concept2.Commands
{
    public abstract class Command
    {
        abstract protected void ReadInternal(ResponseReader reader);

        protected Command(Enums.Csafe id, uint rspSize)
        {
            _id = id;
            _resonseSize = rspSize;
        }

        public void Write(CommandWriter writer)
        {
            writer.WriteByte((uint)_id);
        }

        public void Read(ResponseReader reader)
        {
            uint id = reader.ReadByte();
            uint size = reader.ReadByte();

            if (id == (uint)_id && size == _resonseSize)
                ReadInternal(reader);
            else
                Debug.WriteLine("[Command.Read] id/size mismatch");
        }

        private readonly Enums.Csafe _id;
        private readonly uint _resonseSize;
    }
}
