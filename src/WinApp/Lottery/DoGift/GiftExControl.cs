using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Lottery.DoGift
{
    public partial class GiftExControl : UserControl
    {
        public event EventHandler BtnClick;
        private delegate void handler(string fullname, Image photo);
        private GiftForm ParnetForm;

        /// <summary>
        /// 按钮事件
        /// </summary>
        /// <param name="e"></param>
        protected void OnBtnClick(object sender, EventArgs e)
        {
            if (this.BtnClick != null)
            {
                this.BtnClick(sender, e);
            }
        }

        public void SetInfo(string fullname,Image photo)
        {
            if (!InvokeRequired)
            {
                label1.Text = fullname;
                pictureBox1.Image = photo;
            }
            else
            {
                Invoke(new handler(SetInfo), fullname, photo);
            }
        }

        public GiftExControl(GiftForm parentForm)
        {
            InitializeComponent();
            ParnetForm = parentForm;
            ParnetForm.DoIt += new EventHandler(ParnetForm_DoIt);
        }
        public void SetButtonEnabled(bool enabled)
        {
            button.Enabled = enabled;
        }
        public void SetButtonText()
        {
            button.Text = button.Text.Equals("停止") ? "开始抽奖" : "停止";
        }
        private void button_Click(object sender, EventArgs e)
        {
            
            OnBtnClick(sender,e);
        }

        void ParnetForm_DoIt(object sender, EventArgs e)
        {
            
        }
    }
}
