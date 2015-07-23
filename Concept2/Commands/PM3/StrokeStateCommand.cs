using System;
using Concept2.Enums;

namespace Concept2.Commands.PM3
{
    public class StrokeStateCommand : Pm3Command
    {
        public StrokeStateCommand() : base(Enums.Csafe.PM_GET_STROKESTATE, 1)
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
