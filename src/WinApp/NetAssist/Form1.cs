using Nkit.Core;
using Nkit.Net;
using Nkit.Net.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetAssist
{
    public partial class Form1 : Form
    {
        private UdpClient _udpClient = null;
        private IPEndPoint _remoteEP = null;
        private UdpUtils _udp = null;

        private Socket tcpServer = null;
        private Socket socketServ = null;

        private Socket tcpClient = null;
        
        private Thread threadTcpServer = null;
        private Thread threadTcpClient = null;

        private const int LOCATE_PORT = 50000;
        private const int REMOTE_PORT = 50001;

        private List<string> ips = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ips.Add(NetHelper.getIPAddress() + ":" + LOCATE_PORT);
            ips.Add("127.0.0.1:" + LOCATE_PORT);
            ips.Add("0.0.0.0:" + LOCATE_PORT);

            initUdp();

            initTcpServ();

            initTcpClient();

            gbTcpServer.Visible = false;
            gbTcpClient.Visible = false;
            var p= new Point(7, 71);
            gbTcpServer.Location = p;
            gbTcpClient.Location = p;

        }

        #region UDP
        
        private void initUdp()
        {
            cbUdpLocalIp.Items.Add(NetHelper.getIPAddress() + ":" + LOCATE_PORT);
            cbUdpLocalIp.Items.Add("127.0.0.1:" + LOCATE_PORT);
            cbUdpLocalIp.Items.Add("0.0.0.0:" + LOCATE_PORT);
            cbUdpLocalIp.SelectedIndex = 0;
            cbUdpRemoteIp.Items.Add(NetHelper.getIPAddress() + ":" + REMOTE_PORT);
            cbUdpRemoteIp.Items.Add("127.0.0.1:" + REMOTE_PORT);
            cbUdpRemoteIp.Items.Add("0.0.0.0:" + REMOTE_PORT);
            cbUdpRemoteIp.SelectedIndex = 0;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (btnOpen.Text == "Close")
            {
                btnOpen.Text = "Open";
            }
            else
            {
                var localIps = cbUdpLocalIp.Text.Split(':');
                var port = localIps[1].ToInt(LOCATE_PORT);

                //_udp = new UdpUtils(port);
                //_udp.BeginReceive();
                //_udp.OnDataReceived += udp_OnDataReceived;
                _udpClient = new UdpClient(port);
                _udpClient.Client.Blocking = false;
                var thread = new Thread(Receive)
                {
                    IsBackground = true
                };
                thread.Start();
                btnOpen.Text = "Close";

            }
        }

        private void udp_OnDataReceived(IPEndPoint ep, byte[] buffer)
        {
            if (buffer != null)
            {
                appendCmdMsg(string.Format("RECEIVED FROM {0} ", ep.Address.ToString()));
                appendReceiveData(buffer);
            }
            throw new NotImplementedException();
        }
        private IPEndPoint getRemoteEP()
        {
            var remoteIps = cbUdpRemoteIp.Text.Split(':');
            var port = remoteIps[1].ToInt(REMOTE_PORT);
            if (remoteIps[0] == "0.0.0.0")
                return new IPEndPoint(IPAddress.Any, 0);
            else
                return new IPEndPoint(IPAddress.Parse(remoteIps[0]), port);
        }
        private void Receive()
        {
            _remoteEP = getRemoteEP();
            while (true)
            {
                try
                {
                    if(_udpClient.Client.Available>0)
                    {
                        byte[] buffer = _udpClient.Receive(ref _remoteEP);
                        if (buffer != null)
                        {
                            appendCmdMsg(string.Format("RECEIVED FROM {0}:{1} ", _remoteEP.Address.ToString(), _remoteEP.Port));
                            appendReceiveData(buffer);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        void Send(byte[] buffer)
        {
            try
            {
                _remoteEP = getRemoteEP();
                if (_remoteEP.Port == 0)
                {
                    appendCmdMsg("远端地址无效!");
                    return;
                }
                int r = _udpClient.Send(buffer, buffer.Length, _remoteEP);
                //_udp.SetRemote(remoteIps[0], port);
                //_udp.Send(buffer);
                appendCmdMsg("发送成功! " + r);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        private string buildCmdMsg(string message)
        {
            return string.Format("【{0}】{1}{2}", DateTime.Now.ToString("hh:mm:ss fff"), message, Environment.NewLine);
        }
        private void appendCmdMsg(string message)
        {
            txtCmdMsg.AppendText(buildCmdMsg(message));
        }
        private string buildReceiveData(string message)
        {
            return string.Format("【{0}】{1}{2}", DateTime.Now.ToString("hh:mm:ss fff"), message, Environment.NewLine);
        }
        private void appendReceiveData(byte[] buffer)
        {
            if (buffer == null)
            {
                appendCmdMsg("received null.");
                return;
            }
            var rec = Convertor.Bytes2Hex(buffer);
            if (checkAscii.Checked) rec = Convertor.Bytes2AsciiStr(buffer);
            if (checkStr.Checked) rec = Convertor.Bytes2String(buffer);
            txtReceive.AppendText(buildReceiveData(rec));
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
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

        private void rbUdp_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUdp.Checked)
            {
                gbUdp.Visible = true;
                gbTcpServer.Visible = false;
                gbTcpClient.Visible = false;
            }
        }

        private void rbTcpServer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTcpServer.Checked)
            {
                gbUdp.Visible = false;
                gbTcpServer.Visible = true;
                gbTcpClient.Visible = false;
            }
        }

        private void rbTcpClient_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTcpClient.Checked)
            {
                gbUdp.Visible = false;
                gbTcpServer.Visible = false;
                gbTcpClient.Visible = true;
            }
        }

        private void btnClearRec_Click(object sender, EventArgs e)
        {
            txtReceive.Text = "";
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
        private void checkHexS_CheckedChanged(object sender, EventArgs e)
        {
            var str = txtCmd.Text.Trim();
            if (string.IsNullOrEmpty(str)) return;
            try
            {
                txtCmd.Text = checkHexS.Checked ? Convertor.Bytes2Hex(Encoding.UTF8.GetBytes(str)) : Convertor.Hex2String(str.Split(' '));
            }
            catch (Exception)
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var str = txtCmd.Text.Trim();
                if (string.IsNullOrEmpty(str))
                {
                    appendCmdMsg("数据为空！");
                    return;
                }
                byte[] buffer = checkHexS.Checked? Convertor.Hex2Bytes(str): Convertor.String2Bytes(str);

                if (rbUdp.Checked)
                {
                    if (btnOpen.Text == "Close")
                    {
                        Send(buffer);
                    }
                }
                else if (rbTcpServer.Checked)
                {
                    if (tcpServer != null)
                        TcpServerSend(buffer);
                }
                else if (rbTcpClient.Checked)
                {
                    if (tcpClient != null)
                        TcpClientSend(buffer);
                }
                else
                {
                    appendCmdMsg("invalid handler.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        
        #region TCP Server

        private void initTcpServ()
        {
            cbTcpServLocal.Items.Add(NetHelper.getIPAddress() + ":" + LOCATE_PORT);
            cbTcpServLocal.Items.Add("127.0.0.1:" + LOCATE_PORT);
            cbTcpServLocal.Items.Add("0.0.0.0:" + LOCATE_PORT);
            cbTcpServLocal.SelectedIndex = 0;
        }
        private void btnTcpServ_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnTcpServ.Text == "Close")
                {
                    if (socketServ != null)
                    {
                        tcpServer.Close();
                        socketServ.Close();
                        threadTcpServer.Abort();
                    }
                    btnTcpServ.Text = "Open";
                }
                else
                {
                    var localIps = cbTcpServLocal.Text.Split(':');
                    var port = localIps[1].ToInt(LOCATE_PORT);
                    var localEP = new IPEndPoint(IPAddress.Parse(localIps[0]), port);
                    socketlisten(localEP);
                    btnTcpServ.Text = "Close";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            
        }

        private void TcpServerSend(byte[] buffer)
        {
            int r = tcpServer.Send(buffer, buffer.Length, SocketFlags.None);
            appendCmdMsg("发送成功! " + r);
        }

        void socketlisten(IPEndPoint localEP)
        {
            socketServ = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
            socketServ.Bind(localEP);
            socketServ.Listen(10);

            appendCmdMsg("waiting for a client......");

            threadTcpServer = new Thread(ServerListen)
            {
                IsBackground = true
            };
            threadTcpServer.Start();

        }
        void ServerListen()
        {
            tcpServer = socketServ.Accept();
            var remoteEP = (IPEndPoint)tcpServer.RemoteEndPoint;
            appendCmdMsg("connect with client " + remoteEP.Address + " at port " + remoteEP.Port);

            string welcom = "welcom here!";
            TcpServerSend(Encoding.ASCII.GetBytes(welcom));

            while(true)
            {
                var buffer = new byte[1024];
                var recv = tcpServer.Receive(buffer);
                appendCmdMsg("recv = " + recv);
                if (recv == 0) break;
                appendReceiveData(buffer);
                //appendReceiveData(Encoding.ASCII.GetString(buffer, 0, recv));
                //TcpServerSend(buffer);

            }
            appendCmdMsg("Disconnect from " + remoteEP.Address);
        }


        #endregion

        #region TCP Client

        private void initTcpClient()
        {
            cbTcpClientLocal.Items.Add(NetHelper.getIPAddress() + ":" + LOCATE_PORT);
            cbTcpClientLocal.Items.Add("127.0.0.1:" + LOCATE_PORT);
            cbTcpClientLocal.Items.Add("0.0.0.0:" + LOCATE_PORT);
            cbTcpClientLocal.SelectedIndex = 0;
            cbTcpClientRemote.Items.Add(NetHelper.getIPAddress() + ":" + REMOTE_PORT);
            cbTcpClientRemote.Items.Add("127.0.0.1:" + REMOTE_PORT);
            cbTcpClientRemote.Items.Add("0.0.0.0:" + REMOTE_PORT);
            cbTcpClientRemote.SelectedIndex = 0;
        }
        private void btnClient_Click(object sender, EventArgs e)
        {
            if (btnTcpClient.Text == "DisConnect")
            {
                if (tcpClient != null)
                {
                    tcpClient.Shutdown(SocketShutdown.Both);
                    tcpClient.Close();
                    //tcpClient.Disconnect(true);
                    tcpClient.Dispose();
                    threadTcpClient.Abort();
                }
                btnTcpClient.Text = "Connect";
            }
            else
            {
                var localIps = cbTcpClientLocal.Text.Split(':');
                var localport = localIps[1].ToInt(LOCATE_PORT);
                var localEP = new IPEndPoint(IPAddress.Parse(localIps[0]), localport);
                var remoteIps = cbTcpClientRemote.Text.Split(':');
                var remoteport = remoteIps[1].ToInt(REMOTE_PORT);
                var remoteEP = new IPEndPoint(IPAddress.Parse(remoteIps[0]), remoteport);

                socketconnect(localEP, remoteEP);
                btnTcpClient.Text = "DisConnect";
            }
        }
        private void TcpClientSend(byte[] buffer)
        {
            int r=tcpClient.Send(buffer);
            appendCmdMsg("发送成功! "+r);
        }

        void socketconnect(IPEndPoint localEP, IPEndPoint remoteEP)
        {
            tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //localEP = new IPEndPoint(IPAddress.Any, 60005);
            //tcpClient.Bind(localEP);

            try
            {
                tcpClient.Connect(remoteEP);
            }
            catch (SocketException e)
            {
                throw e;
            }

            threadTcpClient = new Thread(ClientListen)
            {
                IsBackground = true
            };
            threadTcpClient.Start();
        }
        void ClientListen()
        {
            while(true)
            {
                byte[] buffer = new byte[1024];
                int receivedDataLebgth = tcpClient.Receive(buffer);
                appendReceiveData(buffer);
                //appendReceiveData(Encoding.ASCII.GetString(buffer, 0, receivedDataLebgth));
            }
        }

        #endregion

    }
}
