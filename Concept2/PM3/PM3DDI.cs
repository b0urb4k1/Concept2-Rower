using System.Runtime.InteropServices;
using System.Text;

namespace Concept2.PM3
{
    internal partial class NativeMethods
    {
        // tkcmdsetDDI_get_dll_version
        [DllImport(@"PM3\PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetDDI_get_dll_version();

        // tkcmdsetDDI_get_error_name
        [DllImport(@"PM3\PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void tkcmdsetDDI_get_error_name(
            [In] ushort ecode,
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder nameptr,
            [In] ushort namelen);

        // tkcmdsetDDI_get_error_text
        [DllImport(@"PM3\PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void tkcmdsetDDI_get_error_text(
            [In] ushort ecode,
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder textptr,
            [In] ushort textlen);

        // tkcmdsetDDI_init
        [DllImport(@"PM3\PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetDDI_init();

        // tkcmdsetDDI_find_devices
        [DllImport(@"PM3\PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetDDI_find_devices(
            [In] string productName,
            [In, Out] ref byte numFound,
            [In] ushort[] portList);

        // tkcmdsetDDI_discover_pm3s
        [DllImport(@"PM3\PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetDDI_discover_pm3s(
           [In] string productName,
           [In] ushort startingAddress,
           [Out] out ushort numUnits);

        // tkcmdsetDDI_fw_version
        [DllImport(@"PM3\PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetDDI_fw_version(
            [In] ushort port,
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder verPtr,
            [In] ushort verLen);

        // tkcmdsetDDI_hw_version
        [DllImport(@"PM3\PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetDDI_hw_version(
            [In] ushort port,
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder verPtr,
            [In] ushort verLen);

        // tkcmdsetDDI_serial_number
        [DllImport(@"PM3\PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetDDI_serial_number(
            [In] ushort port,
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder serPtr,
            [In] byte serLen);

        // tkcmdsetDDI_status
        [DllImport(@"PM3\PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetDDI_status(
            [In] ushort port,
            [Out] out uint statPtr);

        // tkcmdsetDDI_shutdown
        [DllImport(@"PM3\PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetDDI_shutdown(
            [In] ushort port);

        // tkcmdsetDDI_shutdown_all
        [DllImport(@"PM3\PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetDDI_shutdown_all();
    }
}
