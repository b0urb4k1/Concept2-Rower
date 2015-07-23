using System;

namespace Concept2.Exceptions
{
    [Serializable]
    public class DeviceClosedException : Pm3Exception
    {
        public DeviceClosedException(string message) :
            base(message)
        {
        }
    }

    [Serializable]
    public class InvalidPortException : Pm3Exception
    {
        public InvalidPortException(string message) :
            base(message)
        {
        }
    }

    [Serializable]
    public class ReadTimeoutException : Pm3Exception
    {
        public ReadTimeoutException(string message) :
            base(message)
        {
        }
    }

    [Serializable]
    public class WriteFailedException : Pm3Exception
    {
        public WriteFailedException(string message) :
            base(message)
        {
        }
    }

    [Serializable]
    public class UnknownPm3Exception : Pm3Exception
    {
        public UnknownPm3Exception(string message)
            : base(message)
        {
        }
    }
}
