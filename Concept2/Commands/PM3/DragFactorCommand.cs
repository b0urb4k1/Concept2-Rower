namespace Concept2.Commands.PM3
{
    public class DragFactorCommand : Pm3Command
    {
        public DragFactorCommand() : base(Enums.Csafe.PM_GET_DRAGFACTOR, 1)
        {
            DragFactor = 0;
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            DragFactor = reader.ReadByte();
        }

        public uint DragFactor { get; private set; }
    }
}
