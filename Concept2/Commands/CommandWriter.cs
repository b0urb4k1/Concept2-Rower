using Concept2.Exceptions;

namespace Concept2.Commands
{
    public class CommandWriter
    {
        public CommandWriter(int capacity)
        {
            _buffer = new uint[capacity];
            _size = 0;
        }

        public int Size => _size;
        public uint[] Buffer => _buffer;

        public void Reset()
        {
            _size = 0;
        }

        public void WriteByte(uint val)
        {
            if (_size >= _buffer.Length)
                throw new BufferExceededException("Attempted to write past end of command buffer.");

            _buffer[_size++] = val;
        }

        private readonly uint[] _buffer;
        private int _size;
    }
}
