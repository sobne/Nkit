using System;
using System.Configuration;
using System.Drawing;

namespace Lottery.Services
{
    public class AppSetting
    {
        public string Title { get; set; }
        public string AvatarXml { get; set; }
        public string GiftXml { get; set; }
        public string DefaultPhoto { get; set; }
        public Size ScaleSize { get; set; }
        
        public AppSetting()
        {
            var currentpath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            Title = ConfigurationManager.AppSettings["title"].ToString();
            AvatarXml = currentpath +  ConfigurationManager.AppSettings["avatarXml"].ToString();
            GiftXml = currentpath + ConfigurationManager.AppSettings["giftXml"].ToString();
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
        }
    }
}
