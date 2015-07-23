using Concept2.Types;

namespace Concept2.Commands.PM3
{
    public class WorkTimeCommand : Pm3Command
    {
        public WorkTimeCommand() : base(Enums.Csafe.PM_GET_WORKTIME, 5)
        {
            _time = new Time();
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            _time.TotalHundreths = reader.ReadUInt() + reader.ReadByte();
        }

        public Time WorkTime => _time;

        private Time _time;
    }
}
