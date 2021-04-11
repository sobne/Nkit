using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LotteryChooser.Entity;

namespace LotteryChooser.UserControls
{
    public partial class GiftButtonControl : UserControl
    {
        public delegate void BtnHandler(object sender, GiftArgs e);
        public event BtnHandler BtnClick;
        private IList<Gift> Gifts;
        public Button[] Buttons;
        public GiftButtonControl(IList<Gift> gifts)
        {
            Gifts = gifts;
            Buttons = new Button[gifts.Count];
            InitializeComponent();
        }

        
        protected void OnBtnClick(GiftArgs e)
        {
            if (this.BtnClick != null)
            {
                this.BtnClick(this, e);
            }
        }
        
        public void SetBtnEnabled(int idx,bool enable=false)
        {
            Buttons[idx].Enabled = enable;
        }
        public void SetBtnForeColor(int idx)
        {
            Buttons[idx].ForeColor = Color.DarkGray;
        }

        private void GiftControl_Load(object sender, EventArgs e)
        {
            int i=0;
            int j = 0;
            int l = 0;
            foreach (Gift info in Gifts)
            {
                //if (l == info.Level) j++;
                //else j = 0;
                //l=info.Level;
                Buttons[i] = new Button();
                Buttons[i].Font = new Font("宋体", 14, FontStyle.Regular);
                Buttons[i].Text = info.Title;
                Buttons[i].Left = j * 80+10;
                Buttons[i].Top = (i) * 35 +10 + 5* info.Level;
                //buttonlist[i].Top = (info.Level-1) * 30 +10;
                Buttons[i].Width =180;
                Buttons[i].Height = 35;
                Buttons[i].Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
                Buttons[i].Name = "btn" + i;
                Buttons[i].Click += new EventHandler(btn_Click);
                Buttons[i].BackColor = Color.Transparent;
                Buttons[i].TextAlign = ContentAlignment.MiddleLeft;
                this.Controls.Add(Buttons[i]);
                i++;
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int index = Convert.ToInt32(btn.Name.Replace("btn",""));

            for (int i = 0; i < Gifts.Count; i++)
            {
                Buttons[i].BackColor = i == index ? Color.Khaki : Color.Transparent;
            }

            OnBtnClick(new GiftArgs(Gifts[index]));
        }
    }
}
