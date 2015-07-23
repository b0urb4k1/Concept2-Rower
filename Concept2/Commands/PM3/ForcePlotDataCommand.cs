namespace Concept2.Commands.PM3
{
    public class ForcePlotDataCommand : Pm3Command
    {
        private readonly uint[] _data = new uint[33];

        public ForcePlotDataCommand() : base(Enums.Csafe.PM_GET_FORCEPLOTDATA, 33) {}

        override protected void ReadInternal(ResponseReader reader)
        {
            var length = reader.ReadByte();
            _data[0] = length;

            for (var index = 1; index < 33; index++)
                _data[index] = reader.ReadByte();
        }

        public uint[] Data => _data;
    }
}

