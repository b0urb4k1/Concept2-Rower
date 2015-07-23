using Concept2.Types;

namespace Concept2.Commands.PM3
{
    public class WorkDistanceCommand : Pm3Command
    {
        public WorkDistanceCommand() : base(Enums.Csafe.PM_GET_WORKDISTANCE, 5)
        {
            _distance = new Distance();
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            _distance.TotalTenths = reader.ReadUInt() + reader.ReadByte();
        }

        public Distance WorkDistance => _distance;

        private Distance _distance;
    }
}
