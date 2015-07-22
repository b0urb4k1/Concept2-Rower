using System;
using Concept2.Enums;
using Concept2.Types;

namespace Concept2.Commands.PM3
{
    public class StrokeStateCommand : PM3Command
    {
        public StrokeStateCommand() : base(CSAFE.PM_GET_STROKESTATE, 1)
        {
            StrokeState = StrokeState.Unknown;
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            var val = (StrokeState)reader.ReadByte();

            StrokeState = Enum.IsDefined(typeof(StrokeState), val) ? val : StrokeState.Unknown;
        }

        public StrokeState StrokeState { get; private set; }
    }
}
