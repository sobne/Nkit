using Lottery.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lottery
{
    public partial class GiftControl : UserControl
    {
        public delegate void BtnHandler(object sender, GiftArgs e);
        public event BtnHandler BtnClick;
        private IList<Gift> Gifts;
        public Button[] Buttons;

        public GiftControl(IList<Gift> gifts)
        {
            Gifts = gifts;
            Buttons = new Button[gifts.Count];
            InitializeComponent();
        }
        private void GiftControl_Load(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            int l = 0;
            foreach (Gift m in Gifts)
            {
                //if (l == info.Level) j++;
                //else j = 0;
                //l=info.Level;
                Buttons[i] = new Button();
                Buttons[i].Font = new Font("宋体", 14, FontStyle.Regular);
                Buttons[i].Text = m.Title;
                Buttons[i].Left = j * 80 + 0;
                Buttons[i].Top = i * 35 + 10 + 5 * m.Level;
                //buttonlist[i].Top = (info.Level-1) * 30 +10;
                Buttons[i].Width = 180;
                Buttons[i].Height = 35;
                Buttons[i].Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
                Buttons[i].Name = "btn" + i;
                Buttons[i].Click += new EventHandler(btn_Click);
                Buttons[i].BackColor = Color.Transparent;
                Buttons[i].TextAlign = ContentAlignment.MiddleLeft;
                this.splitContainer1.Panel2.Controls.Add(Buttons[i]);
                i++;
            }
            this.splitContainer1.SplitterDistance = this.splitContainer1.Height - 35 * i + 10;
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int index = Convert.ToInt32(btn.Name.Replace("btn", ""));

            for (int i = 0; i < Gifts.Count; i++)
            {
                Buttons[i].BackColor = i == index ? Color.Khaki : Color.Transparent;
            }
            SetInfo(Gifts[index]);

            OnBtnClick(new GiftArgs(Gifts[index]));
        }

        protected void OnBtnClick(GiftArgs e)
        {
            if (this.BtnClick != null)
            {
                this.BtnClick(this, e);
            }
        }

        private delegate void handler(Gift gift);
        public void SetInfo(Gift gift)
        {
            if (gift == null) return;
            if (!InvokeRequired)
            {
                pictureBox1.Image = gift.Photo;
            }
            else
            {
                Invoke(new handler(SetInfo), gift);
            }
        }
        public void SetBtnEnabled(int idx, bool enable = false)
        {
            Buttons[idx].Enabled = enable;
        }
        public void SetBtnForeColor(int idx)
        {
            Buttons[idx].ForeColor = Color.DarkGray;
        }


    }
}
