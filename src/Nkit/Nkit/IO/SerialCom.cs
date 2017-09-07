using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Nkit.IO
{
    public class SerialCom
    {
        public SerialPort sp;
        //public string PortName { get; set; }
        //public int BaudRate { get; set; }
        //public int DataBits { get; set; }
        //public int StopBits { get; set; }
        //public bool DtrEnable { get; set; }
        //public bool RtsEnable { get; set; }

        public delegate void SerialPorterDataReceiveEventArgs(object sender, SerialDataReceivedEventArgs e, byte[] bits);
        public event SerialPorterDataReceiveEventArgs DataReceived;

        public SerialCom(string portName) : this(portName, 9600) { }
        public SerialCom(string portName,int baudRate):this(portName,baudRate,0,8,1){}
        public SerialCom(string portName, int baudRate, int parity, int dataBits, int stopBits)
            : this(portName, baudRate, parity, dataBits, stopBits, false, false) { }
        public SerialCom(string portName, int baudRate, int parity, int dataBits, int stopBits, bool dtrEnable, bool rtsEnable)
            : this(portName, baudRate, (Parity)parity, dataBits, (StopBits)stopBits, dtrEnable,rtsEnable) { }
        public SerialCom(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
            : this(portName, baudRate, parity, dataBits, stopBits,false,false) { }
        public SerialCom(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits,bool dtrEnable,bool rtsEnable)
        {
            sp = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            sp.DtrEnable = dtrEnable;
            sp.RtsEnable = rtsEnable;
            //sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
        }

        public static string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }
        public void Close()
        {
            try
            {
                if (sp.IsOpen)
                {
                    sp.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Open()
        {
            try
            {
                sp.Open();
                sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                DiscardBuffer();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void DiscardBuffer()
        {
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
        }
        public void write(string text)
        {
            sp.Write(text);
            DiscardBuffer();
            //byte[] b0 = Encoding.Default.GetBytes(dataflow);
            //sp.Write(b0, 0, b0.Length);
        }
        public void write(byte[] buffer)
        {
            sp.Write(buffer, 0, buffer.Length);
            DiscardBuffer();
        }
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(200);
                if (e.EventType == SerialData.Chars)
                {
                    if (sp.BytesToRead <= 0) return;
                    byte[] bytes = new byte[sp.BytesToRead];
                    int count = sp.Read(bytes, 0, bytes.Length);
                    DiscardBuffer();
                    if (DataReceived != null)
                    {
                        DataReceived(sender, e, bytes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private byte[] Receive()
        {
            System.Threading.Thread.Sleep(200);
            if (sp.BytesToRead <= 0) return null;
            byte[] bytes = new byte[sp.BytesToRead];
            int count = sp.Read(bytes, 0, bytes.Length);
            DiscardBuffer();
            //string bs = Encoding.Default.GetString(bytes, 0, count);
            return bytes;
        }
    }
}
