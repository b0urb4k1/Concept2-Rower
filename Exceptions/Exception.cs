﻿using System;
using System.Collections.Generic;
using System.Text;
using Concept2.PM3;

namespace Concept2.Exceptions
{
    [Serializable]
    public class PM3Exception : Exception
    {
        static PM3Exception()
        {
            m_ExceptionMap = new Dictionary<ushort, Type>();
            m_ExceptionMap[55386] = typeof(InvalidPortException);
            m_ExceptionMap[55413] = typeof(DeviceClosedException);
            m_ExceptionMap[55429] = typeof(ReadTimeoutException);
            m_ExceptionMap[55434] = typeof(WriteFailedException);
        }

        private enum PM3Error : ushort
        {
            None = 0,
            DeviceClosed = 1,
            WriteFailed = 2,

            Unknown = ushort.MaxValue
        }

        static private void Throw(ushort error, StringBuilder name, StringBuilder text)
        {
            PM3Exception exception;
            Type type;

            if (m_ExceptionMap.TryGetValue(error, out type))
                exception = (PM3Exception)Activator.CreateInstance(type, text.ToString());
            else
                exception = new UnknownPM3Exception(string.Format("{0} [{1}:{2}]", text, name, error));

            exception.m_Error = error;
            exception.m_Name = name.ToString();
            throw exception;
        }

        static internal void ValidateCsafe(ushort error)
        {
            if (error != 0)
            {
                var name = new StringBuilder(20);
                NativeMethods.tkcmdsetCSAFE_get_error_name(error, name, (ushort)(name.Capacity + 1));

                var text = new StringBuilder(400);
                NativeMethods.tkcmdsetCSAFE_get_error_text(error, text, (ushort)(text.Capacity + 1));

                Throw(error, name, text);
            }
        }

        static internal void ValidateDDI(ushort error)
        {
            if (error != 0)
            {
                var name = new StringBuilder(20);
                NativeMethods.tkcmdsetDDI_get_error_name(error, name, (ushort)(name.Capacity + 1));

                var text = new StringBuilder(400);
                NativeMethods.tkcmdsetDDI_get_error_text(error, text, (ushort)(text.Capacity + 1));

                Throw(error, name, text);
            }
        }

        static internal void ValidateUSB(ushort error)
        {
            if (error != 0)
            {
                var name = new StringBuilder(20);
                NativeMethods.tkcmdsetUSB_get_error_name(error, name, (ushort)(name.Capacity + 1));

                var text = new StringBuilder(400);
                NativeMethods.tkcmdsetUSB_get_error_text(error, text, (ushort)(text.Capacity + 1));

                Throw(error, name, text);
            }
        }

        internal PM3Exception(string message) : base(message) {}

        private static Dictionary<ushort, Type> m_ExceptionMap;
        private ushort m_Error;
        private string m_Name;
    }
}
