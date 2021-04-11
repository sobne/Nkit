using System;
using System.Windows.Forms;

namespace FormatStyler
{
    public partial class FrmMain : Form
    {
        public FrmMain()
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
