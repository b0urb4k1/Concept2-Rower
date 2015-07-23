using System;
using Concept2.Enums;

namespace Concept2.Commands.PM3
{
    public class WorkoutTypeCommand : Pm3Command
    {
        public WorkoutTypeCommand() : base(Enums.Csafe.PM_GET_WORKOUTTYPE, 1)
        {
            WorkoutType = WorkoutType.Unknown;
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            var val = (WorkoutType)reader.ReadByte();
            WorkoutType = Enum.IsDefined(typeof(WorkoutType), val) ? val : WorkoutType.Unknown;
        }

        public WorkoutType WorkoutType { get; private set; }
    }
}
