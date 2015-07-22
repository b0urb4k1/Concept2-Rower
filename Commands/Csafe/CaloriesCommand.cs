using Concept2.Enums;

namespace Concept2.Commands.CSafe
{
    public class CaloriesCommand : Command
    {
        public CaloriesCommand() : base(CSAFE.GETCALORIES_CMD, 2)
        {
            Calories = 0;
        }

        override protected void ReadInternal(ResponseReader reader)
        {
            Calories = reader.ReadUShort();
        }

        public uint Calories { get; private set; }
    }
}
