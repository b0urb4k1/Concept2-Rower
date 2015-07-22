using Concept2.Enums;

namespace Concept2.Commands.CSafe
{
    public class HeartRateCommand : Command
    {
        public HeartRateCommand() : base(CSAFE.GETHRCUR_CMD, 1)
        {
            HeartRate = 0;
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            HeartRate = reader.ReadByte();
        }

        public uint HeartRate { get; private set; }
    }
}
