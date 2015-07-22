using System.Diagnostics;
using Concept2.Exceptions;
using Concept2.Enums;

namespace Concept2
{
    public class Connection : IConnection
    {
        public Connection()
        {
            m_PM3 = new Devices.PM3();
            m_Port = 0;
            m_State = ConnectionState.Disconnected;

            m_PM3.Start();
        }

        public bool IsOpen
        {
            get { return (m_State == ConnectionState.Connected || m_State == ConnectionState.SendError); }
        }

        public ConnectionState State
        {
            get { return m_State; }
        }

        public bool Open()
        {
            if (m_State == ConnectionState.Disconnected)
            {
                int numUnits = m_PM3.DiscoverUnits();
                if (numUnits > 0)
                {
                    try
                    {
                        m_Port = 0;
                        m_State = ConnectionState.Connected;
                    }
                    catch (PM3Exception e)
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
            m_State = ConnectionState.Disconnected;
            m_Port = 0;
        }

        public bool SendCSAFECommand(uint[] cmdData, ushort cmdDataCount, uint[] rspData, ref ushort rspDataCount)
        {
            if (IsOpen)
            {
                try
                {
                    m_PM3.SendCSAFECommand(m_Port, cmdData, cmdDataCount, rspData, ref rspDataCount);
                    return true;
                }
                catch (WriteFailedException e)
                {
                    m_State = ConnectionState.SendError;
                    Debug.WriteLine(string.Format("[Connection.SendCSAFECommand] {0}", e.Message));
                }
                catch (ReadTimeoutException e)
                {
                    m_State = ConnectionState.SendError;
                    Debug.WriteLine(string.Format("[Connection.SendCSAFECommand] {0}", e.Message));
                }
                catch (DeviceClosedException e)
                {
                    m_State = ConnectionState.Disconnected;
                    Debug.WriteLine(string.Format("[Connection.SendCSAFECommand] {0}", e.Message));
                }
            }
            return false;
        }

        private Devices.PM3 m_PM3;
        private ushort m_Port;
        private ConnectionState m_State;
    }
}
