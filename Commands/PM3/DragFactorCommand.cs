using Concept2.Enums;

namespace Concept2.Commands.PM3
{
    public class DragFactorCommand : PM3Command
    {
        public DragFactorCommand() : base(CSAFE.PM_GET_DRAGFACTOR, 1)
        {
            DragFactor = 0;
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            DragFactor = (uint)reader.ReadByte();
        }

        public uint DragFactor { get; private set; }
    }
}
