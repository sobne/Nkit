using LotteryChooser.Utils;
using System;
using System.Configuration;
using System.Drawing;

namespace LotteryChooser.Services
{
    public class AppSetting
    {
        public string Title { get; set; }
        public string Avatar { get; set; }
        public string Gift { get; set; }
        public string DefaultPhoto { get; set; }
        public Size ScaleSize { get; set; }
        public MarginSize MarginSize { get; set; }
        
        public AppSetting()
        {
            var currentpath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            Title = ConfigurationManager.AppSettings["title"].ToString();
            Avatar = currentpath +  ConfigurationManager.AppSettings["avatar"].ToString();
            Gift = currentpath + ConfigurationManager.AppSettings["gift"].ToString();
            DefaultPhoto = currentpath + ConfigurationManager.AppSettings["DefaultPhoto"].ToString();

            var sizeArr= ConfigurationManager.AppSettings["ScaleSize"].ToString().Split(',');
            ScaleSize= new Size(120, 200);
            try
            {
                ScaleSize = new Size(int.Parse(sizeArr[0]), int.Parse(sizeArr[1]));
            }
            catch (Exception)
            {
            }
            var sizeMargin= ConfigurationManager.AppSettings["MarginSize"].ToString().Split(',');
            MarginSize = new MarginSize();
            try
            {
                MarginSize = new MarginSize(int.Parse(sizeMargin[0]), int.Parse(sizeMargin[1]), int.Parse(sizeMargin[2]), int.Parse(sizeMargin[3]));
            }
            catch (Exception)
            {
            }
        }
    }
}
