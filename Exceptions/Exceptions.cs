namespace Concept2.Exceptions
{
    public class DeviceClosedException : PM3Exception
    {
        public DeviceClosedException(string message) :
            base(message)
        {
        }
    }

    public class InvalidPortException : PM3Exception
    {
        public InvalidPortException(string message) :
            base(message)
        {
        }
    }

    public class ReadTimeoutException : PM3Exception
    {
        public ReadTimeoutException(string message) :
            base(message)
        {
        }
    }

    public class WriteFailedException : PM3Exception
    {
        public WriteFailedException(string message) :
            base(message)
        {
        }
    }

    public class UnknownPM3Exception : PM3Exception
    {
        public UnknownPM3Exception(string message)
            : base(message)
        {
        }
    }
}
