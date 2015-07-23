using Concept2.Types;

namespace Concept2.Commands.CSafe
{
    public class PaceCommand : Command
    {
        public PaceCommand() : base(Enums.Csafe.GETPACE_CMD, 3)
        {
            _pace = new Time();
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            uint pace = reader.ReadUShort();
            reader.ReadByte(); // Expecting 0x39 - Seconds per kilometer

            _pace.TotalHundreths = 50 * pace;
        }

        public Time Pace => _pace;

        private Time _pace;
    }
}
