using System.Runtime.InteropServices;
using System.Text;

namespace Concept2.PM3
{
    internal partial class NativeMethods
    {
        // tkcmdsetCSAFE_get_dll_version
        [DllImport(@"PM3\PM3CsafeCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetCSAFE_get_dll_version();

        // tkcmdsetCSAFE_get_error_name
        [DllImport(@"PM3\PM3CsafeCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void tkcmdsetCSAFE_get_error_name(
            [In] ushort erecoderor,
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder nameptr,
            [In] ushort namelen);

        // tkcmdsetCSAFE_get_error_text
        [DllImport(@"PM3\PM3CsafeCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void tkcmdsetCSAFE_get_error_text(
            [In] ushort ecode,
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder textptr,
            [In] ushort textlen);

        // tkcmdsetCSAFE_get_cmd_name
        [DllImport(@"PM3\PM3CsafeCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetCSAFE_get_cmd_name(
            [In] byte cmd,
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder textptr,
            [In] ushort textlen);

        //tkcmdsetCSAFE_get_cmd_text
        [DllImport(@"PM3\PM3CsafeCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetCSAFE_get_cmd_text(
            [In] byte cmd,
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder textptr,
            [In] ushort textlen);

        //tkcmdsetCSAFE_get_cmd_data_types
        [DllImport(@"PM3\PM3CsafeCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetCSAFE_get_cmd_data_types(
            [In] byte cmd,
            [Out] out byte cmdType,
            [In, Out] ref byte numCmdDataTypes,
            [In] byte[] cmdDataType,
            [In, Out] ref byte numRspDataTypes,
            [In] byte[] rspDataType);

        // tkcmdsetCSAFE_init_protocol
        [DllImport(@"PM3\PM3CsafeCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetCSAFE_init_protocol(
            [In] ushort timeout);

        // tkcmdsetCSAFE_command
        [DllImport(@"PM3\PM3CsafeCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetCSAFE_command(
           [In] ushort unitAddress,
           [In] ushort cmdDataSize,
           [In] uint[] cmdData,
           [In, Out] ref ushort rspDataSize,
           [In] uint[] rspData);

        // tkcmdsetCSAFE_get_status
        [DllImport(@"PM3\PM3CsafeCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetCSAFE_get_status();
    }
}
