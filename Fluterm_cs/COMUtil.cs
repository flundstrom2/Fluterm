using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluterm_cs
{
    public class COMUtil : IDisposable
    {
        public COMUtil()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClosePort();
            }
        }

        protected string mCRLF = "\r\n";

        /** Returns sorted list of port names
         */
        public List<string> GetSerialPortNames()
        {
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();
            List<int> sortedPortNos = new List<int>();

            Console.WriteLine("The following serial ports were found:");

            // Display each port name to the console.
            foreach (string port in ports)
            {
                // Tests indicates ports are always named "COMn", always obtained from HKEY_LOCAL_MACHINE\HARDWARE\DEVICEMAP\SERIALCOMM
                int portNo = Convert.ToInt32(port.Substring(3));
                sortedPortNos.Add(portNo);
                // Console.WriteLine(port + ": portNo=" + portNo);
            }
            sortedPortNos.Sort();
            List<string> sortedPortNames = new List<string>();
            foreach (int portNo in sortedPortNos)
            {
                string portName = "COM" + portNo;
                Console.WriteLine("Sorted portName=" + portName);
                sortedPortNames.Add(portName);
            }

            return sortedPortNames;            
        }

        SerialPort mSerialPort = null;
        static int sRxBufferSize = 10;
        static byte[] sRxBuffer = new byte[sRxBufferSize];

        private struct DataChunk
        {
            public byte[] dataRead;
            public int numBytes;
        };

        private static void DataReceivedHandler(
                        object sender,
                        SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            int dataRead = 0;
            int dataToRead = sp.BytesToRead;
            List<DataChunk> readChunks = new List<DataChunk>();
            do
            {
                if (sRxBufferSize < dataToRead)
                {
                    dataToRead = sRxBufferSize;
                }

                // dataToRead shall always be > 0 at this point.
                try
                {
                    dataRead = sp.Read(COMUtil.sRxBuffer, 0, dataToRead);
                } 
                catch (TimeoutException)
                {
                    // Exceptions are expensive in .NET - avoid at all costs!
                    // Console.WriteLine("DataReceivedHandler: TimeoutException");
                    dataRead = 0;
                }
                catch (Exception x)
                {
                    // None of the other documented exceptions can happen if this function is bug-free. Tested 2015-11-03.
                    Console.WriteLine("DataReceivedHandler: Unexpected exception: " + x);
                    return;
                }
                if (dataRead > 0) {
                    DataChunk dc;
                    dc.dataRead = COMUtil.sRxBuffer;
                    dc.numBytes = dataRead;
                    COMUtil.sRxBuffer = new byte[sRxBufferSize];

                    readChunks.Add(dc);
                }
                dataToRead = sp.BytesToRead;
            } while (dataToRead > 0);

            SignalChunksRead(readChunks);
        }

        private static void SignalChunksRead(List<DataChunk> readChunks)
        {
            int chunkIdx = 1;

            Console.WriteLine("Got " + readChunks.Count + " chunks:");
            foreach (DataChunk dc in readChunks)
            {
                string s = ConvertIndataToPrintable(dc.dataRead, dc.numBytes);
                Console.WriteLine(chunkIdx + ": " + dc.numBytes + " bytes: " + s);
                chunkIdx++;
            }
        }

        private static string ConvertIndataToPrintable(byte[] p, int dataRead)
        {
            string s = System.Text.Encoding.UTF8.GetString(p, 0, dataRead);

            return s;

        }

        public bool OpenPort(string portName)
        {
            Console.WriteLine("OpenPort: " + portName);
            ClosePort();
            mSerialPort = new SerialPort(portName);
            mSerialPort.WriteTimeout = 50; // 50ms write timeout. TODO: Reasonable timeout?
            mSerialPort.ReadTimeout = 0; // Finish immediately.

            try
            {
                mSerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                mSerialPort.Open();
                SendString("PING");
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Console.WriteLine("OpenPort: ArgumentOutOfRangeException: Unable to open port (One or more of the properties for this instance are invalid)"); //  For example, the Parity, DataBits, or Handshake properties are not valid values; the BaudRate is less than or equal to zero; the ReadTimeout or WriteTimeout property is less than zero and is not InfiniteTimeout.
                ClosePort();
                return false;
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("OpenPort: ArgumentException: Unable to open port (The port name does not begin with COM or The file type of the port is not supported)");
                ClosePort();
                return false;
            }
            catch (System.UnauthorizedAccessException)
            {
                Console.WriteLine("OpenPort: UnauthorizedAccessException: Unable to open port (Access is denied to the port)"); // Most likely already opened
                ClosePort();
                return false;
            }
            catch (Exception)
            {
                Console.WriteLine("OpenPort: Exception: Unable to open port (Any other exception)");
                ClosePort();
                return false;
            }

            return true;
        }

        private void SendString(string s)
        {
            try
            {
                mSerialPort.Write(s + mCRLF);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("OpenPort: TimeoutException");
            }
        }

        private void ClosePort()
        {
            if (mSerialPort != null)
            {
                mSerialPort.Close();
                mSerialPort.Dispose();
                mSerialPort = null;
            }
        }
    }
}
