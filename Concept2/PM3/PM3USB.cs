﻿using System.Runtime.InteropServices;
using System.Text;

namespace Concept2.PM3
{
    internal partial class NativeMethods
    {
        // PM3Event
        public delegate void Pm3Event(ushort a, byte b);

        // tkcmdsetUSB_get_dll_version
        [DllImport(@"PM3\PM3USBCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetUSB_get_dll_version();

        // tkcmdsetUSB_init
        [DllImport(@"PM3\PM3USBCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetUSB_init();

        // tkcmdsetUSB_get_error_name
        [DllImport(@"PM3\PM3USBCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void tkcmdsetUSB_get_error_name(
            ushort erecoderor,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder nameptr,
            ushort namelen);

        // tkcmdsetUSB_get_error_text
        [DllImport(@"PM3\PM3USBCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void tkcmdsetUSB_get_error_text(
            ushort ecode,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder textptr,
            ushort textlen);

        // tkcmdsetUSB_register_events
        [DllImport(@"PM3\PM3USBCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetUSB_register_events(Pm3Event callback);

        // tkcmdsetUSB_unregister_events
        [DllImport(@"PM3\PM3USBCP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort tkcmdsetUSB_unregister_events();
    }
}
