using System;
using System.Drawing;
using System.Windows.Forms;

namespace LotteryChooser.DoGift
{
    public partial class GiftExControl : UserControl
    {
        public event EventHandler BtnClick;
        private delegate void handler(string fullname, Image img);
        private GiftForm parentFrm;

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
            parentFrm = parentForm;
            parentFrm.DoIt += new EventHandler(ParnetForm_DoIt);
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
