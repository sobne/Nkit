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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void xmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is xmlFrm)
                {
                    frm.Activate();
                    return;
                }
            }
            xmlFrm m = new xmlFrm();
            m.MdiParent = this;
            m.Show();
        }
        private void jsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is json)
                {
                    frm.Activate();
                    return;
                }
            }
            json m = new json();
            m.MdiParent = this;
            m.Show();
        }

        private void unicodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is unicode)
                {
                    frm.Activate();
                    return;
                }
            }
            unicode m = new unicode();
            m.MdiParent = this;
            m.Show();
        }

    }
}
