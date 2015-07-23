using Concept2.Types;

namespace Concept2.Commands.CSafe
{
    public class TimeCommand : Command
    {
        public TimeCommand() : base(Enums.Csafe.GETTWORK_CMD, 3)
        {
            _time = new Time();
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            uint hours = reader.ReadByte();
            uint minutes = reader.ReadByte();
            uint seconds = reader.ReadByte();
            _time.TotalHundreths = 100 * (3600 * hours + 60 * minutes + seconds);
        }

        public Time Time => _time;

        private Time _time;
    }
}
