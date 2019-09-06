using Nkit.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormatType
{
    public partial class unicode : Form
    {
        public unicode()
        {
            InitializeComponent();
        }

        private void toolStripCompose_Click(object sender, EventArgs e)
        {
            textBox2.Text = StringHelper.String2Unicode(textBox1.Text);
        }

        private void toolStripUncode_Click(object sender, EventArgs e)
        {
            textBox1.Text = StringHelper.Unicode2String(textBox2.Text);
        }
    }
}
