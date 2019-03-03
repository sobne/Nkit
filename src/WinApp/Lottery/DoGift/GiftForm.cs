using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using Lottery.Entity;
using Lottery.Services;

namespace Lottery.DoGift
{
    public partial class GiftForm : Form
    {
        public event EventHandler DoIt;
        private System.Timers.Timer timer1;
        private GiftExControl giftBox;
        private IList<Gift> giftspool;
        //private IList<Winner> winners;
        private delegate void Handler();
        Handler handler;

        /// <summary>
        /// 主程序刷新
        /// </summary>
        /// <param name="e"></param>
        protected void OnDoIt(EventArgs e)
        {
            if (this.DoIt != null)
            {
                this.DoIt(this, e);
            }
        }

        public GiftForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {           
            this.timer1 = new System.Timers.Timer(20);
            this.timer1.AutoReset = true;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
            this.timer1.Enabled = false;


            giftBox = new GiftExControl(this);
            giftBox.Dock = DockStyle.Fill;
            giftBox.BtnClick += new EventHandler(giftBox_BtnClick);
            panel1.Controls.Add(giftBox);

            InitData();
        }
        void giftBox_BtnClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (LotteryRuntime.Instance.HasGiftRemain())
            {
                this.timer1.Enabled = !this.timer1.Enabled;

                giftBox.SetButtonText();

                if (!this.timer1.Enabled)
                {
                    Winner wininfo = new Winner(null, giftspool[GetIndex]);
                    //winners.Add(wininfo);
                    LotteryRuntime.Instance.AddWinners(new List<Winner>(){ wininfo });
                }
            }
            else
            {
                disableButton();
            }
        }
        private void disableButton()
        {
            label2.Text="所有奖品已抽完.";
            giftBox.SetButtonEnabled(false);
        }
        private void InitData()
        {
            label2.Text = "正在抽取";

            LotteryRuntime.Instance.Init();
            this.Text = LotteryRuntime.Instance.AppSetting.Title;

            handler = new Handler(LotteryRuntime.Instance.LoadData);
            IAsyncResult result = handler.BeginInvoke(new AsyncCallback(InitComplete), null);
            while (result.IsCompleted)
            {
            }


            this.OnDoIt(new EventArgs());
        }
        void InitComplete(IAsyncResult result)
        {

            Handler handler = (Handler)((AsyncResult)result).AsyncDelegate;

            handler.EndInvoke(result);
            
            refreshData();

            if (!LotteryRuntime.Instance.HasGiftRemain())
            {
                disableButton();
            }
        }
        void refreshData()
        {
            giftspool = LotteryRuntime.Instance.GetGiftsLeft();
            //winners = LotteryRuntime.Instance.GetWinners();
            //if (winners == null) winners = new List<Winner>();
        }
        

        private void timer1_Elapsed(object sender, EventArgs e)
        {
            if (giftspool.Count > 0)
            {
                Random ro = new Random(System.DateTime.Now.Millisecond);
                int index = ro.Next(0, giftspool.Count);
                GetIndex = index;
                giftBox.SetInfo(giftspool[index].Name, giftspool[index].Photo);
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        public int GetIndex
        {
            get;
            set;
        }

    }
}
