using Concept2.Types;
using Concept2.Enums;

namespace Concept2.Commands.CSafe
{
    public class PaceCommand : Command
    {
        public PaceCommand() : base(CSAFE.GETPACE_CMD, 3)
        {
            m_Pace = new Time();
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            uint pace = reader.ReadUShort();
            reader.ReadByte(); // Expecting 0x39 - Seconds per kilometer

            m_Pace.TotalHundreths = 50 * pace;
        }

        public Time Pace => m_Pace;

        private Time m_Pace;
    }
}
