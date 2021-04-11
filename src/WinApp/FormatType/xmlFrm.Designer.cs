namespace FormatStyler
{
    partial class xmlFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(xmlFrm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripCompose = new System.Windows.Forms.ToolStripButton();
            this.toolStripXml2Json = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(800, 425);
            this.textBox1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripCompose,
            this.toolStripXml2Json});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripCompose
            // 
            this.toolStripCompose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.toolStripCompose.Image = ((System.Drawing.Image)(resources.GetObject("toolStripCompose.Image")));
            this.toolStripCompose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCompose.Name = "toolStripCompose";
            this.toolStripCompose.Size = new System.Drawing.Size(64, 22);
            this.toolStripCompose.Text = "格式化";
            this.toolStripCompose.Click += new System.EventHandler(this.toolStripCompose_Click);
            // 
            // toolStripXml2Json
            // 
            this.toolStripXml2Json.Image = ((System.Drawing.Image)(resources.GetObject("toolStripXml2Json.Image")));
            this.toolStripXml2Json.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripXml2Json.Name = "toolStripXml2Json";
            this.toolStripXml2Json.Size = new System.Drawing.Size(84, 22);
            this.toolStripXml2Json.Text = "xml转json";
            this.toolStripXml2Json.Click += new System.EventHandler(this.toolStripXml2Json_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 425);
            this.panel1.TabIndex = 3;
            // 
            // xmlFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "xmlFrm";
            this.Text = "xml";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripCompose;
        private System.Windows.Forms.ToolStripButton toolStripXml2Json;
        private System.Windows.Forms.Panel panel1;
    }
}