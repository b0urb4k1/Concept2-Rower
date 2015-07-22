using Concept2.Types;
using Concept2.Enums;

namespace Concept2.Commands.CSafe
{
    public class TimeCommand : Command
    {
        public TimeCommand() : base(CSAFE.GETTWORK_CMD, 3)
        {
            m_Time = new Time();
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            uint hours = reader.ReadByte();
            uint minutes = reader.ReadByte();
            uint seconds = reader.ReadByte();
            m_Time.TotalHundreths = 100 * (3600 * hours + 60 * minutes + seconds);
        }

        public Time Time => m_Time;

        private Time m_Time;
    }
}
