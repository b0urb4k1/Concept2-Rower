using Concept2.Enums;

namespace Concept2.Commands.CSafe
{
    public class PowerCommand : Command
    {
        public PowerCommand() : base(CSAFE.GETPOWER_CMD, 3)
        {
            Power = 0;
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            Power = reader.ReadUShort();
            reader.ReadByte(); // Expecting 0x58 - Watts
        }

        public uint Power { get; private set; }
    }
}
