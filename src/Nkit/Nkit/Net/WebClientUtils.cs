using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Nkit.Net
{
    public class WebClientUtils
    {
        public WebClient c = null;
        public WebClientUtils()
        {
            c = new WebClient();
        }
        public void DownloadFile(string url, string fileName)
        {
            try
            {
                c.DownloadFile(url, fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DownloadFileAsync(string url, string fileName)
        {
            try
            {
                Uri uri = new Uri(url);
                c.DownloadFileAsync(uri, fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
