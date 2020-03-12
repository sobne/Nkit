namespace HttpRequest
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
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAuthorization = new System.Windows.Forms.TabPage();
            this.cmbAuth = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabHeaders = new System.Windows.Forms.TabPage();
            this.tabBody = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tab2_form_data = new System.Windows.Forms.TabPage();
            this.tab2_x_www_form_urlencoded = new System.Windows.Forms.TabPage();
            this.tab2_raw = new System.Windows.Forms.TabPage();
            this.splitRaw = new System.Windows.Forms.SplitContainer();
            this.cmbRaw = new System.Windows.Forms.ComboBox();
            this.txtRaw = new System.Windows.Forms.TextBox();
            this.tab2_binary = new System.Windows.Forms.TabPage();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1.SuspendLayout();
            this.tabAuthorization.SuspendLayout();
            this.tabBody.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tab2_raw.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitRaw)).BeginInit();
            this.splitRaw.Panel1.SuspendLayout();
            this.splitRaw.Panel2.SuspendLayout();
            this.splitRaw.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbMethod
            // 
            this.cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMethod.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Items.AddRange(new object[] {
            "GET",
            "POST",
            "PUT",
            "DELETE",
            "HEAD",
            "CONNECT",
            "OPTIONS",
            "TRACE"});
            this.cmbMethod.Location = new System.Drawing.Point(4, 5);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(106, 29);
            this.cmbMethod.TabIndex = 0;
            this.cmbMethod.SelectedIndexChanged += new System.EventHandler(this.cmbMethod_SelectedIndexChanged);
            // 
            // txtUrl
            // 
            this.txtUrl.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUrl.Location = new System.Drawing.Point(115, 5);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(568, 29);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "http://";
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("新宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.Location = new System.Drawing.Point(689, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(85, 29);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabAuthorization);
            this.tabControl1.Controls.Add(this.tabHeaders);
            this.tabControl1.Controls.Add(this.tabBody);
            this.tabControl1.Location = new System.Drawing.Point(3, 69);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(444, 399);
            this.tabControl1.TabIndex = 3;
            // 
            // tabAuthorization
            // 
            this.tabAuthorization.Controls.Add(this.cmbAuth);
            this.tabAuthorization.Controls.Add(this.label1);
            this.tabAuthorization.Location = new System.Drawing.Point(4, 22);
            this.tabAuthorization.Name = "tabAuthorization";
            this.tabAuthorization.Padding = new System.Windows.Forms.Padding(3);
            this.tabAuthorization.Size = new System.Drawing.Size(436, 373);
            this.tabAuthorization.TabIndex = 0;
            this.tabAuthorization.Text = "Authorization";
            this.tabAuthorization.UseVisualStyleBackColor = true;
            // 
            // cmbAuth
            // 
            this.cmbAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAuth.FormattingEnabled = true;
            this.cmbAuth.Items.AddRange(new object[] {
            "No Auth",
            "Basic Auth",
            "OAuth 1.0",
            "OAuth 2.0"});
            this.cmbAuth.Location = new System.Drawing.Point(78, 27);
            this.cmbAuth.Name = "cmbAuth";
            this.cmbAuth.Size = new System.Drawing.Size(121, 20);
            this.cmbAuth.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type";
            // 
            // tabHeaders
            // 
            this.tabHeaders.Location = new System.Drawing.Point(4, 22);
            this.tabHeaders.Name = "tabHeaders";
            this.tabHeaders.Padding = new System.Windows.Forms.Padding(3);
            this.tabHeaders.Size = new System.Drawing.Size(436, 373);
            this.tabHeaders.TabIndex = 1;
            this.tabHeaders.Text = "Headers";
            this.tabHeaders.UseVisualStyleBackColor = true;
            // 
            // tabBody
            // 
            this.tabBody.Controls.Add(this.tabControl2);
            this.tabBody.Location = new System.Drawing.Point(4, 22);
            this.tabBody.Name = "tabBody";
            this.tabBody.Padding = new System.Windows.Forms.Padding(3);
            this.tabBody.Size = new System.Drawing.Size(436, 373);
            this.tabBody.TabIndex = 2;
            this.tabBody.Text = "Body";
            this.tabBody.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tab2_form_data);
            this.tabControl2.Controls.Add(this.tab2_x_www_form_urlencoded);
            this.tabControl2.Controls.Add(this.tab2_raw);
            this.tabControl2.Controls.Add(this.tab2_binary);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(430, 367);
            this.tabControl2.TabIndex = 0;
            // 
            // tab2_form_data
            // 
            this.tab2_form_data.Location = new System.Drawing.Point(4, 22);
            this.tab2_form_data.Name = "tab2_form_data";
            this.tab2_form_data.Padding = new System.Windows.Forms.Padding(3);
            this.tab2_form_data.Size = new System.Drawing.Size(422, 341);
            this.tab2_form_data.TabIndex = 0;
            this.tab2_form_data.Text = "form-data";
            this.tab2_form_data.UseVisualStyleBackColor = true;
            // 
            // tab2_x_www_form_urlencoded
            // 
            this.tab2_x_www_form_urlencoded.Location = new System.Drawing.Point(4, 22);
            this.tab2_x_www_form_urlencoded.Name = "tab2_x_www_form_urlencoded";
            this.tab2_x_www_form_urlencoded.Padding = new System.Windows.Forms.Padding(3);
            this.tab2_x_www_form_urlencoded.Size = new System.Drawing.Size(422, 341);
            this.tab2_x_www_form_urlencoded.TabIndex = 1;
            this.tab2_x_www_form_urlencoded.Text = "x-www-form-urlencoded";
            this.tab2_x_www_form_urlencoded.UseVisualStyleBackColor = true;
            // 
            // tab2_raw
            // 
            this.tab2_raw.Controls.Add(this.splitRaw);
            this.tab2_raw.Location = new System.Drawing.Point(4, 22);
            this.tab2_raw.Name = "tab2_raw";
            this.tab2_raw.Size = new System.Drawing.Size(422, 341);
            this.tab2_raw.TabIndex = 2;
            this.tab2_raw.Text = "raw";
            this.tab2_raw.UseVisualStyleBackColor = true;
            // 
            // splitRaw
            // 
            this.splitRaw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRaw.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitRaw.Location = new System.Drawing.Point(0, 0);
            this.splitRaw.Name = "splitRaw";
            this.splitRaw.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitRaw.Panel1
            // 
            this.splitRaw.Panel1.Controls.Add(this.cmbRaw);
            // 
            // splitRaw.Panel2
            // 
            this.splitRaw.Panel2.Controls.Add(this.txtRaw);
            this.splitRaw.Size = new System.Drawing.Size(422, 341);
            this.splitRaw.SplitterDistance = 28;
            this.splitRaw.TabIndex = 2;
            // 
            // cmbRaw
            // 
            this.cmbRaw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRaw.FormattingEnabled = true;
            this.cmbRaw.Items.AddRange(new object[] {
            "Text",
            "Text(text/plain)",
            "JSON(application/json)",
            "Javascript(application/javascript)",
            "XML(application/xml)",
            "XML(text/xml)",
            "HTML(text/html)"});
            this.cmbRaw.Location = new System.Drawing.Point(3, 3);
            this.cmbRaw.Name = "cmbRaw";
            this.cmbRaw.Size = new System.Drawing.Size(248, 20);
            this.cmbRaw.TabIndex = 0;
            // 
            // txtRaw
            // 
            this.txtRaw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRaw.Location = new System.Drawing.Point(0, 0);
            this.txtRaw.Multiline = true;
            this.txtRaw.Name = "txtRaw";
            this.txtRaw.Size = new System.Drawing.Size(422, 309);
            this.txtRaw.TabIndex = 1;
            // 
            // tab2_binary
            // 
            this.tab2_binary.Location = new System.Drawing.Point(4, 22);
            this.tab2_binary.Name = "tab2_binary";
            this.tab2_binary.Size = new System.Drawing.Size(422, 341);
            this.tab2_binary.TabIndex = 3;
            this.tab2_binary.Text = "binary";
            this.tab2_binary.UseVisualStyleBackColor = true;
            // 
            // txtResponse
            // 
            this.txtResponse.BackColor = System.Drawing.SystemColors.Info;
            this.txtResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponse.Location = new System.Drawing.Point(453, 90);
            this.txtResponse.Margin = new System.Windows.Forms.Padding(3, 24, 3, 3);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponse.Size = new System.Drawing.Size(401, 378);
            this.txtResponse.TabIndex = 4;
            // 
            // panelTop
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panelTop, 2);
            this.panelTop.Controls.Add(this.txtUrl);
            this.panelTop.Controls.Add(this.cmbMethod);
            this.panelTop.Controls.Add(this.btnSend);
            this.panelTop.Location = new System.Drawing.Point(3, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(777, 39);
            this.panelTop.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panelTop, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtResponse, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(857, 471);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 471);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "HttpClient";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabAuthorization.ResumeLayout(false);
            this.tabAuthorization.PerformLayout();
            this.tabBody.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tab2_raw.ResumeLayout(false);
            this.splitRaw.Panel1.ResumeLayout(false);
            this.splitRaw.Panel2.ResumeLayout(false);
            this.splitRaw.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitRaw)).EndInit();
            this.splitRaw.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMethod;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAuthorization;
        private System.Windows.Forms.TabPage tabHeaders;
        private System.Windows.Forms.TabPage tabBody;
        private System.Windows.Forms.ComboBox cmbAuth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tab2_form_data;
        private System.Windows.Forms.TabPage tab2_x_www_form_urlencoded;
        private System.Windows.Forms.TabPage tab2_raw;
        private System.Windows.Forms.SplitContainer splitRaw;
        private System.Windows.Forms.ComboBox cmbRaw;
        private System.Windows.Forms.TextBox txtRaw;
        private System.Windows.Forms.TabPage tab2_binary;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

