using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LotteryChooser.Entity;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Linq;
using LotteryChooser.UserControls;
using LotteryChooser.Services;
using LotteryChooser.Utils;

namespace LotteryChooser
{
    public partial class MainForm : Form
    {
        public event EventHandler FrmEvent;
        //private GiftButtonControl giftButtonControl;
        private GiftControl giftControl;
        private LuckerPaintControl luckerPaintControl;
        private IList<Gift> gifts;
        private IList<Winner> winners;
        private IList<Chooser> luckers;
        private IList<Chooser> luckersofgift;
        private delegate void Handler();
        Handler handler;

        private int currentgiftcount;
        Thread thread;
        string running = "0";

        Distribution distribution;

        /// <summary>
        /// 主程序刷新
        /// </summary>
        /// <param name="e"></param>
        protected void OnFrmEvent(EventArgs e)
        {
            this.FrmEvent?.Invoke(this, e);
        }        
        
        //private List<LuckerPaintControl> lcs = new List<LuckerPaintControl>();
        //private void OperateControls(Control control)
        //{
        //    foreach (Control c in control.Controls)
        //    {
        //        if (c is LuckerPaintControl) lcs.Add((LuckerPaintControl)c);
        //        if (c is Panel) OperateControls(c);
        //        if (c is GroupBox) OperateControls(c);
        //    }
        //}


        public MainForm()
        {
            InitializeComponent();
            //WindowState = FormWindowState.Maximized;
            //FormBorderStyle = FormBorderStyle.None;
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

            comboBox1.SelectedIndex = 0;
            comboBox1.Visible = false;
            button1.Visible = false;
                        
            init();
            
            bChose = true;
            thread =new Thread(run);
            thread.Start();
            this.Invalidate();
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
        }


        private void ViewWinEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinListForm winlistform = new WinListForm(gifts, winners);
            winlistform.ShowDialog();
        }
        private void button_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("button_click");
            Button button = sender as Button;
            bChose = !bChose;
            if (LotteryRuntime.Instance.HasGiftAndLuckerRemain())
            {
                //button.Text = button.Text.Equals("停 止") ? "开 始" : "停 止";
                button.Text = bChose ? "开 始" : "停 止";
                //Console.WriteLine("button_click running=" + running);

                if (bChose)
                {
                    new Thread(choseWinner).Start();
                    //new Action(saveWinner).BeginInvoke(null, null);
                }
            }
            else
            {
            }
        }

        private void DisableButton()
        {
            button.Enabled = false;
            if (luckers != null && luckers.Any())
            {
                button1.Visible = true;
                comboBox1.Visible = true;
            }
        }

        public void giftBox_BtnClick(object sender, GiftArgs e)
        {
            if (!bChose) return;
            if (LotteryRuntime.Instance.CurrentGiftId == e.Gift.Id) return;
            LotteryRuntime.Instance.IsAdd = false;
            LotteryRuntime.Instance.SetCurrentGift(e.Gift.Id);
            currentgiftcount = LotteryRuntime.Instance.CurrentGiftCount;
            
            luckersofgift.Clear();
            if (LotteryRuntime.Instance.HasGiftAndLuckerRemain())
            {
                button.Enabled = true;
                button1.Visible = false;
                comboBox1.Visible = false;
                AddCtl(null);
            }
            else
            {
                var cur_winners = winners?.Where(x => x.Gift.Id.Equals(e.Gift.Id));
                if (cur_winners != null && cur_winners.Any())
                {
                    foreach (var item in cur_winners)
                    {
                        luckersofgift.Add(item.Chooser);
                    }
                    AddCtl(luckersofgift);
                }
                giftControl.SetBtnForeColor(LotteryRuntime.Instance.CurrentGiftId);

                DisableButton();
            }
        }



        private void AddCtl(IList<Chooser> list)
        {
            if (list != null && list.Any()) currentgiftcount = list.Count;
            //else currentgiftcount = 20;
            currentgiftcount = currentgiftcount == 0 ? 20 : currentgiftcount;

            if (distribution == null)
            {
                var pw = panel21.Width;
                var ph = panel21.Height;
                //ph = ph * 2 / 3;
                var drawSize = new Size(pw, ph);
                distribution = new Distribution(drawSize, LotteryRuntime.Instance.AppSetting.ScaleSize, LotteryRuntime.Instance.AppSetting.MarginSize);
            }
            distribution.Distribute(currentgiftcount);
            luckerPaintControl = new LuckerPaintControl(distribution.ItemSize, distribution.Points);
            luckerPaintControl.Dock = DockStyle.Fill;
            if (list != null && list.Any())
            {
                luckerPaintControl.SetItems(list);
            }
            else
            {
                //default
            }
            panel21.Visible = false;
            panel21.Controls.Clear();
            panel21.Controls.Add(luckerPaintControl);
            panel21.Visible = true;
        }
        
        private void choseWinner()
        {
            var now = DateTime.Now;
            while (running == "1")
            {
                //Console.WriteLine("thread running");
                Thread.Sleep(10);
                if ((DateTime.Now - now).Seconds > 5) break;
            }
            //OperateControls(panel21);
            try
            {
                LotteryRuntime.Instance.AddLuckers(luckersofgift);
                refreshData();
                if (LotteryRuntime.Instance.HasGiftAndLuckerRemain())
                {

                }
                else
                {
                    DisableButton();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
            }
        }

        private void init()
        {
            LotteryRuntime.Instance.Init();
            this.Text = LotteryRuntime.Instance.AppSetting.Title;
            gifts = LotteryRuntime.Instance.GetGifts();
            luckersofgift = new List<Chooser>();

            handler = new Handler(LotteryRuntime.Instance.LoadData);
            IAsyncResult result = handler.BeginInvoke(new AsyncCallback(initCompleted), null);
            while (result.IsCompleted)
            {
            }

            this.OnFrmEvent(new EventArgs());
        }

        void initCompleted(IAsyncResult result)
        {

            Handler handler = (Handler)((AsyncResult)result).AsyncDelegate;
            
            handler.EndInvoke(result);

            FinalLoad();
            refreshData();
        }
        void refreshData()
        {
            luckers = LotteryRuntime.Instance.GetLuckers();
            winners = LotteryRuntime.Instance.GetWinners();
        }

        void FinalLoad()
        {
            //整段不要
            if (!InvokeRequired)
            {
                //giftButtonControl = new GiftButtonControl(gifts);
                //giftButtonControl.BtnClick += new GiftButtonControl.BtnHandler(giftBox_BtnClick);
                giftControl = new GiftControl(gifts);
                giftControl.BtnClick += new GiftControl.BtnHandler(giftBox_BtnClick);
                giftControl.Dock = DockStyle.Fill;
                //giftControl.Width = 220;
                //giftControl.Height = 300;
                panel1.Controls.Add(giftControl);
            }
            else
            {
                Invoke(new Handler(FinalLoad));
            }

        }


        Boolean bChose = false;

        private void run()
        {
            //整段不要
            while (true)
            {
                if (bChose)
                {
                    Thread.Sleep(100);
                    continue;
                }
                if (luckers.Count > 0)
                {
                    Random ro = new Random(Guid.NewGuid().GetHashCode());
                    int t1 = Environment.TickCount;
                    //lock (running)
                    //{
                    running = "1";
                    Console.WriteLine("running1:" + running);
                    Random rd = new Random(Guid.NewGuid().GetHashCode());
                    var boxs = new List<int>();
                    for (int i = 0; i < currentgiftcount; i++)
                    {
                        boxs.Add(i);
                    }
                    List<int> winIndexs = new List<int>();
                    luckersofgift.Clear();

                    for (int i = 0; i < currentgiftcount; i++)
                    {
                        if (winIndexs.Count == luckers.Count) break;
                        int index = ro.Next(0, luckers.Count);
                        while (winIndexs.Contains(index))
                        {
                            index = ro.Next(0, luckers.Count);
                        }
                        if (winIndexs.Count >= currentgiftcount) winIndexs.RemoveAt(0);
                        winIndexs.Add(index);
                        luckersofgift.Add(luckers[index]);
                    }

                    luckerPaintControl.SetItems(luckersofgift);
                    Thread.Sleep(2);
                    running = "0";
                    Console.WriteLine("running0:" + running);
                    //}
                    //label3.Text = (Environment.TickCount - t1).ToString();

                }
                else
                {
                    //MessageBox.Show("全都中奖，纳尼？");
                }

                //Thread.Sleep(150);
            }
        }



        #region search 1 lucker in luckers 不要

        private void ToolStripMenuItem_lucker1_Click(object sender, EventArgs e)
        {
            searchLuckerInLuckers();
        }
        private void label1_Click(object sender, EventArgs e)
        {
            searchLuckerInLuckers();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (luckers == null || !luckers.Any())
            {
                MessageBox.Show("全部已中奖...");
                return;
            }
            var newnum = comboBox1.SelectedItem.ToString().Replace("个", "");
            currentgiftcount = int.Parse(newnum);
            LotteryRuntime.Instance.IsAdd = true;
            AddCtl(null);
            button.Enabled = true;
        }
        /// <summary>
        /// 奖中奖
        /// </summary>
        private void searchLuckerInLuckers()
        {
            if (LotteryRuntime.Instance.IsAdd) return;

            var list = new List<Chooser>();
            foreach (var item in luckersofgift)
            {
                if (item == null) return;
                if (item.Remark != "0")
                {
                    list.Add(item);
                }
            }
            if (!list.Any())
            {
                MessageBox.Show(":( 当前中奖的幸运者都不在线...");
                return;
            }

            Random ro = new Random(Guid.NewGuid().GetHashCode());
            int index = ro.Next(0, list.Count);

            AddCtl(new List<Chooser> { list[index] });
        }


        #endregion
    }
}
