/* License
 * This work is licensed under the Creative Commons Attribution 2.5 License
 * http://creativecommons.org/licenses/by/2.5/

 * You are free:
 *     to copy, distribute, display, and perform the work
 *     to make derivative works
 *     to make commercial use of the work
 * 
 * Under the following conditions:
 *     You must attribute the work in the manner specified by the author or licensor:
 *          - If you find this component useful a email to [sobne.cn@gmail.com] would be appreciated.
 *     For any reuse or distribution, you must make clear to others the license terms of this work.
 *     Any of these conditions can be waived if you get permission from the copyright holder.
 * 
 * Copyright sobne.cn All Rights Reserved.
*/
using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Caching;
using System.Net;
using System.IO;

namespace Nkit.Web
{

    public class ControlAccesor
    {
        /// <summary>
        /// 在服务器端控制HTML控件的显示
        /// </summary>
        /// <param name="ClientID">Client ID</param>
        /// <param name="Visible">true显示,false隐藏</param>
        /// <param name="page">页面</param>
        public static void ShowCtrl(string ClientID, bool Visible, Page page)
        {
            StringBuilder script = new StringBuilder();
            script.Append("<script language=javascript>");
            script.AppendFormat("document.getElementById ('{0}').style.display='{1}';", ClientID, Visible ? "block" : "none");
            script.Append("</script>");
            JScriptHelper.RegistClientScript(page, "showCtrl", script.ToString());
        }
    }
}
