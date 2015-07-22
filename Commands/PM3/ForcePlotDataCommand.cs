using System;
using Concept2.Enums;

namespace Concept2.Commands.PM3
{
    public class ForcePlotDataCommand : PM3Command
    {
        private uint[] m_Data = new uint[33];

        public ForcePlotDataCommand() : base(CSAFE.PM_GET_FORCEPLOTDATA, 33) {}

        override protected void ReadInternal(ResponseReader reader)
        {
            var length = reader.ReadByte();
            m_Data[0] = length;

            for (var index = 1; index < 33; index++)
                m_Data[index] = reader.ReadByte();
        }

        public uint[] Data {get{ return m_Data; }}
    }
}

