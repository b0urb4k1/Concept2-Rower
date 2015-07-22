using Concept2.Enums;

namespace Concept2.Commands.PM3
{
    public abstract class PM3Command : Command
    {
        protected PM3Command(CSAFE id, uint rspSize) : base(id, rspSize) {}
    }
}
