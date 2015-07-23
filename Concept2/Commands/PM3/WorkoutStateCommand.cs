using System;
using Concept2.Enums;

namespace Concept2.Commands.PM3
{
    public class WorkoutStateCommand : Pm3Command
    {
        public WorkoutStateCommand() : base(Enums.Csafe.PM_GET_WORKOUTSTATE, 1)
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
