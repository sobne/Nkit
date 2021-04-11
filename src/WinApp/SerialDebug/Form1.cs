using System;
using System.Windows.Forms;
using System.IO.Ports;
using Nkit.IO;
using Nkit.Core;

namespace SerialDebug
{
    public partial class Form1 : Form
    {
        private static SerialCom _spr;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            txtCmd.Text = "68 01 fe 68 00 06 49 bf d7 0d";


            foreach (string port in SerialCom.GetPortNames())
            {
                cbPortName.Items.Add(port);
            }
            string[] bauds = new string[] { "9600", "19200", "115200" };
            foreach (string baud in bauds)
            {
                cbBaudRate.Items.Add(baud);
            }
            foreach (string name in Enum.GetNames(typeof(Parity)))
            {
                cbParity.Items.Add(name);
            }
            for (int i = 5; i < 9; i++)
            {
                cbDataBits.Items.Add(i);
            }
            foreach (string name in Enum.GetNames(typeof(StopBits)))
            {
                cbStopBits.Items.Add(name);
            }

            cbBaudRate.SelectedIndex = 0;
            cbDataBits.SelectedIndex = 3;
            cbParity.SelectedIndex = 0;
            if (cbPortName.Items.Count > 0) cbPortName.SelectedIndex = 0;
            cbStopBits.SelectedIndex = 1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string str = txtCmd.Text.Trim();
            if (string.IsNullOrEmpty(str))
            {
                txtCmdMsg.Text = "数据为空！";
                return;
            }
            int dataBits = 0;
            if (checkHexS.Checked)
            {
                byte[] cmd = Convertor.Hex2Bytes(str);
                _spr.write(cmd);
                dataBits = cmd.Length;
            }
            else
            {
                byte[] cmd = Convertor.String2Bytes(str);
                _spr.write(str);
                dataBits = cmd.Length;
            }
            
            txtCmdMsg.Text = "发送成功:"+dataBits;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (btnOpen.Text == "Close")
            {
                _spr.Close();
                btnOpen.Text = "Open";

            }
            else
            {
                string portName = cbPortName.Text;
                int baudRate = cbBaudRate.Text.ToInt32(9600);
                Parity parity = (Parity)Enum.Parse(typeof(Parity),cbParity.Text);
                
                int dataBits = cbDataBits.Text.ToInt32(8);
                StopBits stopBits = (StopBits)Enum.Parse(typeof(StopBits), cbStopBits.Text);
                //StopBits stopBits = (StopBits)1;
                _spr = new SerialCom(portName, baudRate, parity, dataBits, stopBits);
                _spr.Open();
                _spr.DataReceived += new SerialCom.SerialPorterDataReceiveEventArgs(_spr_DataReceived);
                //_spr.sp.DataReceived += new SerialDataReceivedEventHandler(spr_DataReceived);
                btnOpen.Text = "Close";
 
            }
        }

        void spr_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Chars)
            {
                //byte[] bytes = _spr.Receive();
                //string rec = checkHexR.Checked ? Convertor.Bytes2Hex(bytes) : Convertor.Bytes2AsciiStr(bytes);
                //listBox1.Items.Add(rec);
            }
            //throw new NotImplementedException();
        }
        void _spr_DataReceived(object sender, SerialDataReceivedEventArgs e, byte[] bits)
        {
            if (bits == null)
            {
                txtCmdMsg.Text = "received null.";
                return;
            }
            string rec = Convertor.Bytes2Hex(bits);
            if (checkAscii.Checked) rec = Convertor.Bytes2AsciiStr(bits);
            if (checkStr.Checked) rec = Convertor.Bytes2String(bits);
            listBox1.Items.Add(rec);
            //throw new NotImplementedException();
        }

        private void btnClearRec_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void btnClearS_Click(object sender, EventArgs e)
        {
            txtCmd.Text = "";
        }

        private void checkHexR_CheckedChanged(object sender, EventArgs e)
        {
            //checkAscii.Checked = !checkHexR.Checked;
            CheckBox_CheckedChanged(sender, e);
        }

        private void checkAscii_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox_CheckedChanged(sender, e);
            //checkHexR.Checked = !checkAscii.Checked;
        }
        private void checkStr_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox_CheckedChanged(sender, e);
        }
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb=sender as CheckBox;
            if (cb.Checked)
            {
                foreach (Control c in cb.Parent.Controls)
                {
                    if (c is CheckBox)
                    {
                        CheckBox chk = c as CheckBox;
                        if (chk != sender)
                            chk.Checked = false;
                    }
                }
            }
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    _spr.sp.RtsEnable = !_spr.sp.RtsEnable;
        //    _spr.sp.DtrEnable = !_spr.sp.DtrEnable;
        //    listBox1.Items.Add("rts:" + _spr.sp.RtsEnable.ToString());
        //    listBox1.Items.Add("dtr:" + _spr.sp.DtrEnable.ToString());
        //}

    }
}
