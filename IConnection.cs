using Concept2.Enums;

namespace Concept2
{
    public interface IConnection
    {
        bool IsOpen { get; }
        ConnectionState State { get; }
        bool Open();
        void Close();
        bool SendCSAFECommand(uint[] cmdData, ushort cmdDataCount, uint[] rspData, ref ushort rspDataCount);
    }
}
