using Concept2.Enums;
using Concept2.Types;

namespace Concept2.Commands.PM3
{
    public class WorkTimeCommand : PM3Command
    {
        public WorkTimeCommand() : base(CSAFE.PM_GET_WORKTIME, 5)
        {
            m_Time = new Time();
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            m_Time.TotalHundreths = reader.ReadUInt() + reader.ReadByte();
        }

        public Time WorkTime { get { return m_Time; } }

        private Time m_Time;
    }
}
