using System;
using Concept2.Exceptions;

namespace Concept2.Commands
{
    public class ResponseReader
    {
        public ResponseReader(int capacity)
        {
            _buffer = new uint[capacity];
            _size = 0;
            _position = 0;
        }

        public ushort Capacity => (ushort)_buffer.Length;
        public int Size => _size;
        public int Position => _position;
        public uint[] Buffer => _buffer;

        public void Reset(int size)
        {
            _size = Math.Min(size, _buffer.Length);
            _position = 0;
        }

        public uint ReadByte()
        {
            if (_position >= _size)
            {
                throw new BufferExceededException("Attempted to read past end of response buffer.");
            }

            return _buffer[_position++];
        }

        public uint ReadUShort()
        {
            uint val = (ReadByte() << 0) + (ReadByte() << 8);
            return val;
        }

        public uint ReadUInt()
        {
            uint val = (ReadByte() << 0) + (ReadByte() << 8) + (ReadByte() << 16) + (ReadByte() << 24);
            return val;
        }

        private readonly uint[] _buffer;
        private int _size;
        private int _position;
    }
}
