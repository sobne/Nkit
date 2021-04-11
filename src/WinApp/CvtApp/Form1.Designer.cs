namespace CvtApp
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtStr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAscii = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHex = new System.Windows.Forms.TextBox();
            this.txtBin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "string";
            // 
            // txtStr
            // 
            this.txtStr.Location = new System.Drawing.Point(60, 10);
            this.txtStr.Name = "txtStr";
            this.txtStr.Size = new System.Drawing.Size(325, 21);
            this.txtStr.TabIndex = 1;
            this.txtStr.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtStr_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "ascii";
            // 
            // txtAscii
            // 
            this.txtAscii.Location = new System.Drawing.Point(60, 47);
            this.txtAscii.Name = "txtAscii";
            this.txtAscii.Size = new System.Drawing.Size(325, 21);
            this.txtAscii.TabIndex = 1;
            this.txtAscii.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAscii_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "hex";
            // 
            // txtHex
            // 
            this.txtHex.Location = new System.Drawing.Point(60, 84);
            this.txtHex.Name = "txtHex";
            this.txtHex.Size = new System.Drawing.Size(325, 21);
            this.txtHex.TabIndex = 1;
            this.txtHex.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtHex_KeyUp);
            // 
            // txtBin
            // 
            this.txtBin.Location = new System.Drawing.Point(60, 120);
            this.txtBin.Name = "txtBin";
            this.txtBin.Size = new System.Drawing.Size(325, 21);
            this.txtBin.TabIndex = 3;
            this.txtBin.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBin_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "bin";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 168);
            this.Controls.Add(this.txtBin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtHex);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAscii);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStr);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "编码转换";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAscii;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHex;
        private System.Windows.Forms.TextBox txtBin;
        private System.Windows.Forms.Label label4;
    }
}

