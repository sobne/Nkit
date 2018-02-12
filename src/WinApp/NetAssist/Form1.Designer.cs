namespace NetAssist
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtCmdMsg = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkHexS = new System.Windows.Forms.CheckBox();
            this.btnClearS = new System.Windows.Forms.Button();
            this.txtCmd = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.gbUdp = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbUdpRemoteIp = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.cbUdpLocalIp = new System.Windows.Forms.ComboBox();
            this.gp1 = new System.Windows.Forms.GroupBox();
            this.checkStr = new System.Windows.Forms.CheckBox();
            this.checkAscii = new System.Windows.Forms.CheckBox();
            this.checkHexR = new System.Windows.Forms.CheckBox();
            this.btnClearRec = new System.Windows.Forms.Button();
            this.gbTcpServer = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTcpServLocal = new System.Windows.Forms.ComboBox();
            this.btnTcpServ = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbTcpClient = new System.Windows.Forms.RadioButton();
            this.rbTcpServer = new System.Windows.Forms.RadioButton();
            this.rbUdp = new System.Windows.Forms.RadioButton();
            this.gbTcpClient = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbTcpClientRemote = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnTcpClient = new System.Windows.Forms.Button();
            this.cbTcpClientLocal = new System.Windows.Forms.ComboBox();
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            this.gbUdp.SuspendLayout();
            this.gp1.SuspendLayout();
            this.gbTcpServer.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbTcpClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCmdMsg
            // 
            this.txtCmdMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCmdMsg.Location = new System.Drawing.Point(6, 98);
            this.txtCmdMsg.Multiline = true;
            this.txtCmdMsg.Name = "txtCmdMsg";
            this.txtCmdMsg.ReadOnly = true;
            this.txtCmdMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCmdMsg.Size = new System.Drawing.Size(450, 64);
            this.txtCmdMsg.TabIndex = 24;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtCmdMsg);
            this.groupBox3.Controls.Add(this.checkHexS);
            this.groupBox3.Controls.Add(this.btnClearS);
            this.groupBox3.Controls.Add(this.txtCmd);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(179, 254);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(466, 168);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "发送区";
            // 
            // checkHexS
            // 
            this.checkHexS.AutoSize = true;
            this.checkHexS.Checked = true;
            this.checkHexS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkHexS.Location = new System.Drawing.Point(22, 20);
            this.checkHexS.Name = "checkHexS";
            this.checkHexS.Size = new System.Drawing.Size(96, 16);
            this.checkHexS.TabIndex = 10;
            this.checkHexS.Text = "十六进制发送";
            this.checkHexS.UseVisualStyleBackColor = true;
            this.checkHexS.CheckedChanged += new System.EventHandler(this.checkHexS_CheckedChanged);
            // 
            // btnClearS
            // 
            this.btnClearS.Location = new System.Drawing.Point(124, 16);
            this.btnClearS.Name = "btnClearS";
            this.btnClearS.Size = new System.Drawing.Size(76, 23);
            this.btnClearS.TabIndex = 9;
            this.btnClearS.Text = "清除";
            this.btnClearS.UseVisualStyleBackColor = true;
            this.btnClearS.Click += new System.EventHandler(this.btnClearS_Click);
            // 
            // txtCmd
            // 
            this.txtCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCmd.Location = new System.Drawing.Point(6, 45);
            this.txtCmd.Multiline = true;
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.Size = new System.Drawing.Size(355, 42);
            this.txtCmd.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(373, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gbUdp
            // 
            this.gbUdp.Controls.Add(this.label3);
            this.gbUdp.Controls.Add(this.cbUdpRemoteIp);
            this.gbUdp.Controls.Add(this.label1);
            this.gbUdp.Controls.Add(this.label2);
            this.gbUdp.Controls.Add(this.btnOpen);
            this.gbUdp.Controls.Add(this.cbUdpLocalIp);
            this.gbUdp.Location = new System.Drawing.Point(7, 71);
            this.gbUdp.Name = "gbUdp";
            this.gbUdp.Size = new System.Drawing.Size(166, 140);
            this.gbUdp.TabIndex = 22;
            this.gbUdp.TabStop = false;
            this.gbUdp.Text = "UDP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "远端";
            // 
            // cbUdpRemoteIp
            // 
            this.cbUdpRemoteIp.FormattingEnabled = true;
            this.cbUdpRemoteIp.Location = new System.Drawing.Point(6, 110);
            this.cbUdpRemoteIp.Name = "cbUdpRemoteIp";
            this.cbUdpRemoteIp.Size = new System.Drawing.Size(152, 20);
            this.cbUdpRemoteIp.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "本地";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(40, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "主机地址：端口号";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(84, 70);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(76, 23);
            this.btnOpen.TabIndex = 8;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // cbUdpLocalIp
            // 
            this.cbUdpLocalIp.FormattingEnabled = true;
            this.cbUdpLocalIp.Location = new System.Drawing.Point(8, 44);
            this.cbUdpLocalIp.Name = "cbUdpLocalIp";
            this.cbUdpLocalIp.Size = new System.Drawing.Size(152, 20);
            this.cbUdpLocalIp.TabIndex = 12;
            // 
            // gp1
            // 
            this.gp1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gp1.Controls.Add(this.txtReceive);
            this.gp1.Controls.Add(this.checkStr);
            this.gp1.Controls.Add(this.checkAscii);
            this.gp1.Controls.Add(this.checkHexR);
            this.gp1.Controls.Add(this.btnClearRec);
            this.gp1.Location = new System.Drawing.Point(179, 6);
            this.gp1.Name = "gp1";
            this.gp1.Size = new System.Drawing.Size(466, 240);
            this.gp1.TabIndex = 21;
            this.gp1.TabStop = false;
            this.gp1.Text = "接收区";
            // 
            // checkStr
            // 
            this.checkStr.AutoSize = true;
            this.checkStr.Location = new System.Drawing.Point(124, 20);
            this.checkStr.Name = "checkStr";
            this.checkStr.Size = new System.Drawing.Size(42, 16);
            this.checkStr.TabIndex = 12;
            this.checkStr.Text = "Str";
            this.checkStr.UseVisualStyleBackColor = true;
            this.checkStr.CheckedChanged += new System.EventHandler(this.checkStr_CheckedChanged);
            // 
            // checkAscii
            // 
            this.checkAscii.AutoSize = true;
            this.checkAscii.Location = new System.Drawing.Point(67, 20);
            this.checkAscii.Name = "checkAscii";
            this.checkAscii.Size = new System.Drawing.Size(54, 16);
            this.checkAscii.TabIndex = 11;
            this.checkAscii.Text = "Ascii";
            this.checkAscii.UseVisualStyleBackColor = true;
            this.checkAscii.CheckedChanged += new System.EventHandler(this.checkAscii_CheckedChanged);
            // 
            // checkHexR
            // 
            this.checkHexR.AutoSize = true;
            this.checkHexR.Checked = true;
            this.checkHexR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkHexR.Location = new System.Drawing.Point(22, 20);
            this.checkHexR.Name = "checkHexR";
            this.checkHexR.Size = new System.Drawing.Size(42, 16);
            this.checkHexR.TabIndex = 10;
            this.checkHexR.Text = "Hex";
            this.checkHexR.UseVisualStyleBackColor = true;
            this.checkHexR.CheckedChanged += new System.EventHandler(this.checkHexR_CheckedChanged);
            // 
            // btnClearRec
            // 
            this.btnClearRec.Location = new System.Drawing.Point(172, 15);
            this.btnClearRec.Name = "btnClearRec";
            this.btnClearRec.Size = new System.Drawing.Size(76, 23);
            this.btnClearRec.TabIndex = 9;
            this.btnClearRec.Text = "清除";
            this.btnClearRec.UseVisualStyleBackColor = true;
            this.btnClearRec.Click += new System.EventHandler(this.btnClearRec_Click);
            // 
            // gbTcpServer
            // 
            this.gbTcpServer.Controls.Add(this.label5);
            this.gbTcpServer.Controls.Add(this.label4);
            this.gbTcpServer.Controls.Add(this.cbTcpServLocal);
            this.gbTcpServer.Controls.Add(this.btnTcpServ);
            this.gbTcpServer.Location = new System.Drawing.Point(7, 316);
            this.gbTcpServer.Name = "gbTcpServer";
            this.gbTcpServer.Size = new System.Drawing.Size(166, 88);
            this.gbTcpServer.TabIndex = 24;
            this.gbTcpServer.TabStop = false;
            this.gbTcpServer.Text = "Tcp Server";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(32, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "(主机地址：端口号)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "本地";
            // 
            // cbTcpServLocal
            // 
            this.cbTcpServLocal.FormattingEnabled = true;
            this.cbTcpServLocal.Location = new System.Drawing.Point(8, 31);
            this.cbTcpServLocal.Name = "cbTcpServLocal";
            this.cbTcpServLocal.Size = new System.Drawing.Size(152, 20);
            this.cbTcpServLocal.TabIndex = 15;
            // 
            // btnTcpServ
            // 
            this.btnTcpServ.Location = new System.Drawing.Point(83, 58);
            this.btnTcpServ.Name = "btnTcpServ";
            this.btnTcpServ.Size = new System.Drawing.Size(76, 23);
            this.btnTcpServ.TabIndex = 8;
            this.btnTcpServ.Text = "Open";
            this.btnTcpServ.UseVisualStyleBackColor = true;
            this.btnTcpServ.Click += new System.EventHandler(this.btnTcpServ_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbTcpClient);
            this.groupBox4.Controls.Add(this.rbTcpServer);
            this.groupBox4.Controls.Add(this.rbUdp);
            this.groupBox4.Location = new System.Drawing.Point(7, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(166, 59);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            // 
            // rbTcpClient
            // 
            this.rbTcpClient.Location = new System.Drawing.Point(69, 31);
            this.rbTcpClient.Name = "rbTcpClient";
            this.rbTcpClient.Size = new System.Drawing.Size(91, 22);
            this.rbTcpClient.TabIndex = 25;
            this.rbTcpClient.Text = "Tcp Client";
            this.rbTcpClient.UseVisualStyleBackColor = true;
            this.rbTcpClient.CheckedChanged += new System.EventHandler(this.rbTcpClient_CheckedChanged);
            // 
            // rbTcpServer
            // 
            this.rbTcpServer.Location = new System.Drawing.Point(69, 11);
            this.rbTcpServer.Name = "rbTcpServer";
            this.rbTcpServer.Size = new System.Drawing.Size(91, 22);
            this.rbTcpServer.TabIndex = 24;
            this.rbTcpServer.Text = "Tcp Server";
            this.rbTcpServer.UseVisualStyleBackColor = true;
            this.rbTcpServer.CheckedChanged += new System.EventHandler(this.rbTcpServer_CheckedChanged);
            // 
            // rbUdp
            // 
            this.rbUdp.AutoSize = true;
            this.rbUdp.Checked = true;
            this.rbUdp.Location = new System.Drawing.Point(8, 14);
            this.rbUdp.Name = "rbUdp";
            this.rbUdp.Size = new System.Drawing.Size(41, 16);
            this.rbUdp.TabIndex = 23;
            this.rbUdp.TabStop = true;
            this.rbUdp.Text = "UDP";
            this.rbUdp.UseVisualStyleBackColor = true;
            this.rbUdp.CheckedChanged += new System.EventHandler(this.rbUdp_CheckedChanged);
            // 
            // gbTcpClient
            // 
            this.gbTcpClient.Controls.Add(this.label6);
            this.gbTcpClient.Controls.Add(this.cbTcpClientRemote);
            this.gbTcpClient.Controls.Add(this.label7);
            this.gbTcpClient.Controls.Add(this.label8);
            this.gbTcpClient.Controls.Add(this.btnTcpClient);
            this.gbTcpClient.Controls.Add(this.cbTcpClientLocal);
            this.gbTcpClient.Location = new System.Drawing.Point(7, 217);
            this.gbTcpClient.Name = "gbTcpClient";
            this.gbTcpClient.Size = new System.Drawing.Size(166, 93);
            this.gbTcpClient.TabIndex = 26;
            this.gbTcpClient.TabStop = false;
            this.gbTcpClient.Text = "Tcp Client";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "远端";
            // 
            // cbTcpClientRemote
            // 
            this.cbTcpClientRemote.FormattingEnabled = true;
            this.cbTcpClientRemote.Location = new System.Drawing.Point(8, 37);
            this.cbTcpClientRemote.Name = "cbTcpClientRemote";
            this.cbTcpClientRemote.Size = new System.Drawing.Size(152, 20);
            this.cbTcpClientRemote.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "本地";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(32, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "(主机地址：端口号)";
            // 
            // btnTcpClient
            // 
            this.btnTcpClient.Location = new System.Drawing.Point(84, 63);
            this.btnTcpClient.Name = "btnTcpClient";
            this.btnTcpClient.Size = new System.Drawing.Size(76, 23);
            this.btnTcpClient.TabIndex = 8;
            this.btnTcpClient.Text = "Connect";
            this.btnTcpClient.UseVisualStyleBackColor = true;
            this.btnTcpClient.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // cbTcpClientLocal
            // 
            this.cbTcpClientLocal.FormattingEnabled = true;
            this.cbTcpClientLocal.Location = new System.Drawing.Point(8, 123);
            this.cbTcpClientLocal.Name = "cbTcpClientLocal";
            this.cbTcpClientLocal.Size = new System.Drawing.Size(152, 20);
            this.cbTcpClientLocal.TabIndex = 12;
            // 
            // txtReceive
            // 
            this.txtReceive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceive.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtReceive.Location = new System.Drawing.Point(6, 42);
            this.txtReceive.Multiline = true;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.ReadOnly = true;
            this.txtReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReceive.Size = new System.Drawing.Size(450, 185);
            this.txtReceive.TabIndex = 25;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 426);
            this.Controls.Add(this.gbTcpClient);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.gbTcpServer);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbUdp);
            this.Controls.Add(this.gp1);
            this.Name = "Form1";
            this.Text = "网络调试助手";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbUdp.ResumeLayout(false);
            this.gbUdp.PerformLayout();
            this.gp1.ResumeLayout(false);
            this.gp1.PerformLayout();
            this.gbTcpServer.ResumeLayout(false);
            this.gbTcpServer.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gbTcpClient.ResumeLayout(false);
            this.gbTcpClient.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCmdMsg;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkHexS;
        private System.Windows.Forms.Button btnClearS;
        private System.Windows.Forms.TextBox txtCmd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gbUdp;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.ComboBox cbUdpLocalIp;
        private System.Windows.Forms.GroupBox gp1;
        private System.Windows.Forms.CheckBox checkStr;
        private System.Windows.Forms.CheckBox checkAscii;
        private System.Windows.Forms.CheckBox checkHexR;
        private System.Windows.Forms.Button btnClearRec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbUdpRemoteIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbTcpServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbTcpServLocal;
        private System.Windows.Forms.Button btnTcpServ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox gbTcpClient;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbTcpClientRemote;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnTcpClient;
        private System.Windows.Forms.ComboBox cbTcpClientLocal;
        private System.Windows.Forms.RadioButton rbUdp;
        private System.Windows.Forms.RadioButton rbTcpClient;
        private System.Windows.Forms.RadioButton rbTcpServer;
        private System.Windows.Forms.TextBox txtReceive;
    }
}

