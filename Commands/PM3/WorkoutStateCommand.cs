using System;
using Concept2.Enums;
using Concept2.Types;

namespace Concept2.Commands.PM3
{
    public class WorkoutStateCommand : PM3Command
    {
        public WorkoutStateCommand() : base(CSAFE.PM_GET_WORKOUTSTATE, 1)
        {
            WorkoutState = WorkoutState.Unknown;
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            var val = (WorkoutState)reader.ReadByte();
            WorkoutState = Enum.IsDefined(typeof(WorkoutState), val) ? val : WorkoutState.Unknown;
        }

        public WorkoutState WorkoutState { get; private set; }
    }
}
