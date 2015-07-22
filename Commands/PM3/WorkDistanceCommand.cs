using Concept2.Enums;
using Concept2.Types;

namespace Concept2.Commands.PM3
{
    public class WorkDistanceCommand : PM3Command
    {
        public WorkDistanceCommand() : base(CSAFE.PM_GET_WORKDISTANCE, 5)
        {
            m_Distance = new Distance();
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            m_Distance.TotalTenths = reader.ReadUInt() + reader.ReadByte();
        }

        public Distance WorkDistance => m_Distance;

        private Distance m_Distance;
    }
}
