using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Nkit.Web
{
    public class MetaUtils
    {
        public const string Page_Enter = "Page-Enter";
        public const string Page_Exist = "Page-Exist";
        public const string Expires = "Expires";
        public const string Cache_Control = "Cache-Control";
        public const string Pragma = "Pragma";

        public string HttpEquiv { get; set; }

        public string Content { get; set; }


        public HtmlMeta Meta()
        {
            HtmlMeta meta = new HtmlMeta();
            meta.HttpEquiv = HttpEquiv;
            meta.Content = Content;
            return meta;
        }
    }



    #region HtmlMeta方法
    public class PageEnter
    {
    }
    public class PageExit
    {

    }
    public class ContentType
    {

    }
    public class Refresh
    {

    }
    public class Expires
    {

    }
    public class Pragma
    {

    }
    public class SetCookie
    {

    }
    public class Windowtarget
    {

    }
    public class Picslabel
    {

    }
    #endregion
}
