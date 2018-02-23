namespace EnCrypt
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
            this.radAes = new System.Windows.Forms.RadioButton();
            this.radBase64 = new System.Windows.Forms.RadioButton();
            this.radDes = new System.Windows.Forms.RadioButton();
            this.radShift = new System.Windows.Forms.RadioButton();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radAes
            // 
            this.radAes.AutoSize = true;
            this.radAes.Location = new System.Drawing.Point(13, 13);
            this.radAes.Name = "radAes";
            this.radAes.Size = new System.Drawing.Size(41, 16);
            this.radAes.TabIndex = 0;
            this.radAes.TabStop = true;
            this.radAes.Text = "AES";
            this.radAes.UseVisualStyleBackColor = true;
            // 
            // radBase64
            // 
            this.radBase64.AutoSize = true;
            this.radBase64.Location = new System.Drawing.Point(81, 13);
            this.radBase64.Name = "radBase64";
            this.radBase64.Size = new System.Drawing.Size(59, 16);
            this.radBase64.TabIndex = 1;
            this.radBase64.TabStop = true;
            this.radBase64.Text = "Base64";
            this.radBase64.UseVisualStyleBackColor = true;
            // 
            // radDes
            // 
            this.radDes.AutoSize = true;
            this.radDes.Location = new System.Drawing.Point(167, 12);
            this.radDes.Name = "radDes";
            this.radDes.Size = new System.Drawing.Size(41, 16);
            this.radDes.TabIndex = 2;
            this.radDes.TabStop = true;
            this.radDes.Text = "DES";
            this.radDes.UseVisualStyleBackColor = true;
            // 
            // radShift
            // 
            this.radShift.AutoSize = true;
            this.radShift.Location = new System.Drawing.Point(235, 13);
            this.radShift.Name = "radShift";
            this.radShift.Size = new System.Drawing.Size(53, 16);
            this.radShift.TabIndex = 3;
            this.radShift.TabStop = true;
            this.radShift.Text = "Shift";
            this.radShift.UseVisualStyleBackColor = true;
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(13, 36);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(389, 85);
            this.txtInput.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(199, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Encode";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(12, 156);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(389, 96);
            this.txtOutput.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(304, 127);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Decode";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 264);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.radShift);
            this.Controls.Add(this.radDes);
            this.Controls.Add(this.radBase64);
            this.Controls.Add(this.radAes);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radAes;
        private System.Windows.Forms.RadioButton radBase64;
        private System.Windows.Forms.RadioButton radDes;
        private System.Windows.Forms.RadioButton radShift;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button button2;
    }
}

