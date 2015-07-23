using System;
using System.Collections.Generic;
using System.Text;
using Concept2.PM3;

namespace Concept2.Exceptions
{
    [Serializable]
    public class Pm3Exception : Exception
    {
        static Pm3Exception()
        {
            ExceptionMap = new Dictionary<ushort, Type>();
            ExceptionMap[55386] = typeof(InvalidPortException);
            ExceptionMap[55413] = typeof(DeviceClosedException);
            ExceptionMap[55429] = typeof(ReadTimeoutException);
            ExceptionMap[55434] = typeof(WriteFailedException);
        }

        static private void Throw(ushort error, StringBuilder name, StringBuilder text)
        {
            Pm3Exception exception;
            Type type;

            if (ExceptionMap.TryGetValue(error, out type))
                exception = (Pm3Exception)Activator.CreateInstance(type, text.ToString());
            else
                exception = new UnknownPm3Exception(string.Format("{0} [{1}:{2}]", text, name, error));

            exception._error = error;
            exception._name = name.ToString();
            throw exception;
        }

        static internal void ValidateCsafe(ushort error)
        {
            if (error == 0) return;

            var name = new StringBuilder(20);
            NativeMethods.tkcmdsetCSAFE_get_error_name(error, name, (ushort)(name.Capacity + 1));

            var text = new StringBuilder(400);
            NativeMethods.tkcmdsetCSAFE_get_error_text(error, text, (ushort)(text.Capacity + 1));

            Throw(error, name, text);
        }

        static internal void ValidateDdi(ushort error)
        {
            if (error == 0) return;

            var name = new StringBuilder(20);
            NativeMethods.tkcmdsetDDI_get_error_name(error, name, (ushort)(name.Capacity + 1));

            var text = new StringBuilder(400);
            NativeMethods.tkcmdsetDDI_get_error_text(error, text, (ushort)(text.Capacity + 1));

            Throw(error, name, text);
        }

        static internal void ValidateUsb(ushort error)
        {
            if (error == 0) return;

            var name = new StringBuilder(20);
            NativeMethods.tkcmdsetUSB_get_error_name(error, name, (ushort)(name.Capacity + 1));

            var text = new StringBuilder(400);
            NativeMethods.tkcmdsetUSB_get_error_text(error, text, (ushort)(text.Capacity + 1));

            Throw(error, name, text);
        }

        internal Pm3Exception(string message) : base(message) {}

        private static readonly Dictionary<ushort, Type> ExceptionMap;
        private ushort _error;
        private string _name;
    }
}
