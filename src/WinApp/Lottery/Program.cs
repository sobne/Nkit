using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LotteryChooser
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var lt = ConfigurationManager.AppSettings["LotteryType"].ToString();
            if (lt == "1") Application.Run(new DoGift.GiftForm());
            else Application.Run(new MainForm());
            //buildLucker();
        }


        static void buildLucker()
        {
            var sp = @"C:\face";
            DirectoryInfo Dir = new DirectoryInfo(sp);
            DirectoryInfo[] DirSub = Dir.GetDirectories();

            var sb0 = new StringBuilder();
            sb0.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sb0.Append("<list>");
            var i = 1;
            foreach (DirectoryInfo d in DirSub)
            {
                var images = d.GetFiles();
                var sb = new StringBuilder();
                foreach (var item in images)
                {
                    if (item.Name.ToLower().EndsWith(".jpg") || item.Name.ToLower().EndsWith(".png"))
                    {
                        sb.Append("<info id=\"" + i + "\">");
                        sb.Append("<name>" + item.Name + "</name>");
                        sb.Append("<department>" + d.Name + "</department>");
                        sb.Append("<pic>" + item.FullName + "</pic>");
                        sb.Append("<remark></remark>");
                        sb.Append("</info>");
                        i++;
                    }
                }
                sb0.Append(sb.ToString());

            }
            sb0.Append("</list>");
            
            var s = sb0.ToString();
        }
    }
}
