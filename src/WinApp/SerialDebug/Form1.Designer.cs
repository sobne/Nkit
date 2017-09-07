namespace SerialDebug
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.txtCmd = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClearRec = new System.Windows.Forms.Button();
            this.gp1 = new System.Windows.Forms.GroupBox();
            this.checkStr = new System.Windows.Forms.CheckBox();
            this.checkAscii = new System.Windows.Forms.CheckBox();
            this.checkHexR = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkHexS = new System.Windows.Forms.CheckBox();
            this.btnClearS = new System.Windows.Forms.Button();
            this.txtCmdMsg = new System.Windows.Forms.TextBox();
            this.gp1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(374, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCmd
            // 
            this.txtCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCmd.Location = new System.Drawing.Point(6, 45);
            this.txtCmd.Multiline = true;
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.Size = new System.Drawing.Size(356, 42);
            this.txtCmd.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(6, 44);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(453, 184);
            this.listBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "端 口";
            // 
            // cbPortName
            // 
            this.cbPortName.FormattingEnabled = true;
            this.cbPortName.Location = new System.Drawing.Point(55, 23);
            this.cbPortName.Name = "cbPortName";
            this.cbPortName.Size = new System.Drawing.Size(76, 20);
            this.cbPortName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "波特率";
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Location = new System.Drawing.Point(55, 48);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(76, 20);
            this.cbBaudRate.TabIndex = 7;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(55, 149);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(76, 23);
            this.btnOpen.TabIndex = 8;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
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
            // gp1
            // 
            this.gp1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gp1.Controls.Add(this.checkStr);
            this.gp1.Controls.Add(this.checkAscii);
            this.gp1.Controls.Add(this.checkHexR);
            this.gp1.Controls.Add(this.btnClearRec);
            this.gp1.Controls.Add(this.listBox1);
            this.gp1.Location = new System.Drawing.Point(162, 1);
            this.gp1.Name = "gp1";
            this.gp1.Size = new System.Drawing.Size(466, 240);
            this.gp1.TabIndex = 10;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "校验位";
            // 
            // cbParity
            // 
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Location = new System.Drawing.Point(55, 73);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(76, 20);
            this.cbParity.TabIndex = 12;
            // 
            // cbDataBits
            // 
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Location = new System.Drawing.Point(55, 98);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(76, 20);
            this.cbDataBits.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "数据位";
            // 
            // cbStopBits
            // 
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(55, 123);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(76, 20);
            this.cbStopBits.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "停止位";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbPortName);
            this.groupBox2.Controls.Add(this.cbStopBits);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbBaudRate);
            this.groupBox2.Controls.Add(this.cbDataBits);
            this.groupBox2.Controls.Add(this.btnOpen);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbParity);
            this.groupBox2.Location = new System.Drawing.Point(4, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(152, 181);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "串口设置";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.checkHexS);
            this.groupBox3.Controls.Add(this.btnClearS);
            this.groupBox3.Controls.Add(this.txtCmd);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(163, 254);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(467, 97);
            this.groupBox3.TabIndex = 19;
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
            // txtCmdMsg
            // 
            this.txtCmdMsg.Location = new System.Drawing.Point(4, 188);
            this.txtCmdMsg.Multiline = true;
            this.txtCmdMsg.Name = "txtCmdMsg";
            this.txtCmdMsg.ReadOnly = true;
            this.txtCmdMsg.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtCmdMsg.Size = new System.Drawing.Size(152, 162);
            this.txtCmdMsg.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 358);
            this.Controls.Add(this.txtCmdMsg);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gp1);
            this.Name = "Form1";
            this.Text = "串口调试工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gp1.ResumeLayout(false);
            this.gp1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtCmd;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPortName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClearRec;
        private System.Windows.Forms.GroupBox gp1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkHexR;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkHexS;
        private System.Windows.Forms.Button btnClearS;
        private System.Windows.Forms.TextBox txtCmdMsg;
        private System.Windows.Forms.CheckBox checkAscii;
        private System.Windows.Forms.CheckBox checkStr;
    }
}

