using System;

namespace Concept2.Exceptions
{
    public class CommandSetException : Exception
    {
        public CommandSetException(string message) :
            base(message)
        {
        }
    }

    public class BufferExceededException : Exception
    {
        public BufferExceededException(string message) :
            base(message)
        {
        }
    }
}
