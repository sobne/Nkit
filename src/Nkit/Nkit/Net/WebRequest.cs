using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Nkit.Net
{
    public class WebRequestUtil
    {
        private HttpWebRequest _req;
        public WebRequestUtil()
        {
            
        }
        public string FetchPage(string url)
        {
            String result;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Encoding enc = Encoding.UTF8;
            using (StreamReader sr = new StreamReader(res.GetResponseStream(), enc))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }
    }
}
