using System.Text;
using Concept2.Exceptions;
using Concept2.PM3;

namespace Concept2.Devices
{
    public class PM3
    {
        static public string s_ProductName = "Concept2 Performance Monitor 3 (PM3)";

        public void Start()
        {
            ushort error;

            error = NativeMethods.tkcmdsetDDI_init();
            PM3Exception.ValidateDDI(error);

            error = NativeMethods.tkcmdsetCSAFE_init_protocol(1000);
            PM3Exception.ValidateCsafe(error);
        }

        public void Stop()
        {
            var error = NativeMethods.tkcmdsetDDI_shutdown_all();
            PM3Exception.ValidateDDI(error);
        }

        public int DiscoverUnits()
        {
            ushort numUnits;
            var error = NativeMethods.tkcmdsetDDI_discover_pm3s(s_ProductName, 0, out numUnits);
            PM3Exception.ValidateDDI(error);

            return numUnits;
        }

        public UnitInfo GetUnitInfo(int port)
        {
            ushort error;

            var hwVersion = new StringBuilder(20);
            error = NativeMethods.tkcmdsetDDI_hw_version((ushort)port, hwVersion, (ushort)(hwVersion.Capacity + 1));
            PM3Exception.ValidateDDI(error);

            var fwVersion = new StringBuilder(20);
            error = NativeMethods.tkcmdsetDDI_fw_version((ushort)port, fwVersion, (ushort)(fwVersion.Capacity + 1));
            PM3Exception.ValidateDDI(error);

            var serialNumber = new StringBuilder(16);
            error = NativeMethods.tkcmdsetDDI_serial_number((ushort)port, serialNumber, (byte)(serialNumber.Capacity + 1));
            PM3Exception.ValidateDDI(error);

            var info = new UnitInfo
                { hwVersion = hwVersion.ToString()
                , fwVersion = fwVersion.ToString()
                , serialNumber = serialNumber.ToString()
                };
            
            return info;
        }

        public void SendCSAFECommand(ushort port, uint[] cmdData, ushort cmdDataCount, uint[] rspData, ref ushort rspDataCount)
        {
            var error = NativeMethods.tkcmdsetCSAFE_command(port, cmdDataCount, cmdData, ref rspDataCount, rspData);
            var name = new StringBuilder(20);
            NativeMethods.tkcmdsetCSAFE_get_error_name(error, name, (ushort)(name.Capacity + 1));

            var text = new StringBuilder(400);
            NativeMethods.tkcmdsetCSAFE_get_error_text(error, text, (ushort)(text.Capacity + 1));

            //PM3Exception.ValidateCsafe(error);
        }
    }
}
