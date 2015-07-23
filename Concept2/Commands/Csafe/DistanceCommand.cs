using Concept2.Types;

namespace Concept2.Commands.Csafe
{
    public class DistanceCommand : Command
    {
        public DistanceCommand() : base(Enums.Csafe.GETHORIZONTAL_CMD, 3)
        {
            _distance = new Distance();
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            _distance.TotalTenths = 10 * reader.ReadUShort();
            reader.ReadByte(); // Expecting 0x24 - Metres
        }

        public Distance Distance => _distance;

        private Distance _distance;
    }
}
