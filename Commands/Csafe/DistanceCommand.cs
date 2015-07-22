using Concept2.Enums;
using Concept2.Types;

namespace Concept2.Commands.PM3
{
    public class DistanceCommand : Command
    {
        public DistanceCommand() : base(CSAFE.GETHORIZONTAL_CMD, 3)
        {
            m_Distance = new Distance();
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            m_Distance.TotalTenths = 10 * reader.ReadUShort();
            reader.ReadByte(); // Expecting 0x24 - Metres
        }

        public Distance Distance { get { return m_Distance; } }

        private Distance m_Distance;
    }
}
