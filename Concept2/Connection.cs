using System.Diagnostics;
using Concept2.Exceptions;
using Concept2.Enums;

namespace Concept2
{
    public class Connection : IConnection
    {
        public Connection()
        {
            _pm3 = new Devices.Pm3();
            _port = 0;
            _state = ConnectionState.Disconnected;

            _pm3.Start();
        }

        public bool IsOpen => (_state == ConnectionState.Connected || _state == ConnectionState.SendError);

        public ConnectionState State => _state;

        public bool Open()
        {
            if (_state == ConnectionState.Disconnected)
            {
                int numUnits = _pm3.DiscoverUnits();
                if (numUnits > 0)
                {
                    try
                    {
                        _port = 0;
                        _state = ConnectionState.Connected;
                    }
                    catch (Pm3Exception e)
                    {
                        Debug.WriteLine(string.Format("[Connection.Open] {0}", e.Message));
                    }
                }
            }
            else
            {
                Debug.WriteLine("[Connection.Open] Connection already open");
            }

            return IsOpen;
        }

        public void Close()
        {
            _state = ConnectionState.Disconnected;
            _port = 0;
        }

        public bool SendCsafeCommand(uint[] cmdData, ushort cmdDataCount, uint[] rspData, ref ushort rspDataCount)
        {
            if (IsOpen)
            {
                try
                {
                    _pm3.SendCsafeCommand(_port, cmdData, cmdDataCount, rspData, ref rspDataCount);
                    return true;
                }
                catch (WriteFailedException e)
                {
                    _state = ConnectionState.SendError;
                    Debug.WriteLine(string.Format("[Connection.SendCSAFECommand] {0}", e.Message));
                }
                catch (ReadTimeoutException e)
                {
                    _state = ConnectionState.SendError;
                    Debug.WriteLine(string.Format("[Connection.SendCSAFECommand] {0}", e.Message));
                }
                catch (DeviceClosedException e)
                {
                    _state = ConnectionState.Disconnected;
                    Debug.WriteLine(string.Format("[Connection.SendCSAFECommand] {0}", e.Message));
                }
            }
            return false;
        }

        private readonly Devices.Pm3 _pm3;
        private ushort _port;
        private ConnectionState _state;
    }
}
