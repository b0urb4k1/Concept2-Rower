namespace Concept2.Commands.PM3
{
    public abstract class Pm3Command : Command
    {
        protected Pm3Command(Enums.Csafe id, uint rspSize) : base(id, rspSize) {}
    }
}
