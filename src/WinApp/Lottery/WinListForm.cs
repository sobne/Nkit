using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LotteryChooser.Entity;

namespace LotteryChooser
{
    public partial class WinListForm : Form
    {
        IList<Winner> winList;
        IList<Gift> giftList;
       // private string[] winpeople;//中奖人员名单
        private Label[] winpeopleLabel;//中奖人员名单显示Label

        public WinListForm(IList<Gift> giftlist,IList<Winner> winlist)
        {
            winList = winlist;
            giftList = giftlist;
           // winpeople = new string[giftlist.Count];
            winpeopleLabel = new Label[giftlist.Count];
            InitializeComponent();
        }

        private void WinListForm_Load(object sender, EventArgs e)
        {

            int i = 0;

            foreach (Gift info in giftList)
            {
                Font font = new System.Drawing.Font("宋体", 14, FontStyle.Bold);
                
                Label giftnameLabel = new Label();
                giftnameLabel.Text = info.Title;
                giftnameLabel.Top = 80 * i;
                giftnameLabel.Left = 20;
                giftnameLabel.Font = font;
                giftnameLabel.AutoSize = true;
                panel1.Controls.Add(giftnameLabel);
                
                winpeopleLabel[i] = new Label();
                winpeopleLabel[i].Font= new System.Drawing.Font("宋体", 12, FontStyle.Bold);
                winpeopleLabel[i].ForeColor = Color.White;//Color.FromArgb(255, 255, 128, 0) ;
                winpeopleLabel[i].Top = 80 * (i) + 26;
                winpeopleLabel[i].Left = 20;
                winpeopleLabel[i].Height = 50;
                winpeopleLabel[i].Width = this.Width - 80;
                panel1.Controls.Add(winpeopleLabel[i]);

                i++;
            }

            
            
            foreach (Winner info in winList)
            {
                int winindex=(int)info.Gift.Id;
                winpeopleLabel[winindex].Text = info.Chooser.Name + "   " + winpeopleLabel[winindex].Text;
            }
        }
    }
}
