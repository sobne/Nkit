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

namespace Nkit.Web
{
    /// <summary>
    /// javaScript帮助类
    /// </summary>
    public class JScriptHelper
    {
        /// <summary>
        /// 注册页面客户端脚本
        /// </summary>
        /// <param name="P">页面</param>
        /// <param name="sKey">Key:'KEY','close',etc...</param>
        /// <param name="allCommand">js脚本语句</param>
        public static void RegistClientScript(Page P, string sKey, string allCommand)
        {
            ClientScriptManager CSM = P.ClientScript;
            if (!CSM.IsStartupScriptRegistered(P.GetType(), sKey))
                CSM.RegisterStartupScript(P.GetType(), sKey, allCommand);
        }
        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="message">窗口提示信息</param>
        public static void Alert(Page page, string message)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language='javascript' type='text/javascript'>");
            builder.Append("window.alert('");
            builder.Append(message);
            builder.Append("');");
            builder.Append("</script>");
            RegistClientScript(page, "KEY", builder.ToString());
        }
        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="message">消息内</param>
        /// <param name="toUrl">连接地址</param>
        public static void AlertAndRedirect(Page page, string message, string toUrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language='javascript' type='text/javascript'>");
            builder.Append("alert('" + message + "');");
            builder.Append("window.location = '" + toUrl + "';");
            builder.Append("</script>");
            RegistClientScript(page, "close", builder.ToString());
        }
        /// <summary>
        /// 弹出确认窗口
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="message">确认信息</param>
        public static void Confirm(Page page, string message)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language='javascript' type='text/javascript'>");
            builder.Append("return confirm('");
            builder.Append(message);
            builder.Append("');");
            builder.Append("</script>");
            RegistClientScript(page, "KEY", builder.ToString());
        }
        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        /// <param name="page">页面</param>
        public static void CloseWindow(Page page)
        {
            string str = "<script language='JavaScript'>window.close();</script>";
            RegistClientScript(page, "close", str);
        }
        /// <summary>
        /// 屏蔽提示的关闭窗口
        /// </summary>
        /// <param name="page">页面</param>
        public static void CloseWindowWithOutTrip(Page page)
        { 
            StringBuilder sb=new StringBuilder();
            sb.Append("<script language='javascript' type='text/javascript'>");
            sb.Append("var ua=navigator.userAgent;");
            sb.Append("var ie=navigator.appName=='Microsoft Internet Explorer'?true:false;");
            sb.Append("if(ie){");
            sb.Append("    var IEversion=parseFloat(ua.substring(ua.indexOf('MSIE ')+5,ua.indexOf(';',ua.indexOf('MSIE ')))) ;");
            sb.Append("    if(IEversion< 5.5){ ");
            sb.Append("        var str = '<object id=noTipClose classid='clsid:ADB880A6-D8FF-11CF-9377-00AA003B7A11'>' ;");
            sb.Append("        str += '<param name='Command' value='Close'></object>'; ");
            sb.Append("        document.body.insertAdjacentHTML('beforeEnd', str); ");
            sb.Append("        document.all.noTipClose.Click(); ");
            sb.Append("    } ");
            sb.Append("    else{"); 
            sb.Append("        window.opener =null;");
            sb.Append("        window.open('','_self','');//for IE7");
            sb.Append("        window.close(); ");
            sb.Append("    } ");
            sb.Append("} ");
            sb.Append("else ");
            sb.Append("    window.close();");
            sb.Append("</script>");
            RegistClientScript(page, "colse", sb.ToString());
        }
        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="message">窗口信息</param>
        public static void Alert(string message)
        {
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }
        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="toUrl">连接地址</param>
        public static void AlertAndRedirect(string message, string toUrl)
        {
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toUrl));
        }
        /// <summary>
        /// 回到历史页面
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value)
        {
            string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow()
        {
            string js = @"<Script language='JavaScript'>
                    parent.opener=null;window.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 刷新父窗口
        /// </summary>
        /// <param name="url">父窗口地址</param>
        public static void RefreshParent(string url)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.location.href='" + url + "';window.close();</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        public static void RefreshOpener()
        {
            string js = @"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }
        /// <summary>
        /// 打开指定大小的新窗体
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="width">宽</param>
        /// <param name="heigth">高</param>
        /// <param name="top">头位置</param>
        /// <param name="left">左位置</param>
        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
        {
            string js = @"<Script language='JavaScript'>
                            window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + @",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');
                          </Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 转向Url制定的页面
        /// </summary>
        /// <param name="url">连接地址</param>
        public static void JavaScriptLocationHref(string url)
        {
            string js = @"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 打开指定大小位置的模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="top">距离上位置</param>
        /// <param name="left">距离左位置</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features);
        }
        /// <summary>
        /// 打开模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="features">格式设置 like:'status:no;scroll=yes'</param>
        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            string js = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(js);
        }
        /// <summary>
        /// 形成打开模式窗口脚本
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="features">格式设置 like:'status:no;scroll=yes'</param>
        /// <returns>Javascript脚本字符串</returns>
        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            return @"<script language=javascript>							
							showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
        }
        /// <summary>
        /// 打开全屏窗口
        /// </summary>
        /// <param name="P">页面</param>
        /// <param name="Url">打开页面地址</param>
        public static void OpenFullScreenWindow(Page P, string Url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language='javascript' type='text/javascript'>");
            sb.Append("var sFeatures='fullscreen=0,toolbar=0,location=0,locationbar=0,directories=0,status=1,statusbar=1,menubar=0,scrollbars=0,resizable=1,top=0,left=0';");
            sb.Append("window.open("+Url+",new Date().getTime(),sFeatures,false);");
            sb.Append("window.opener =null;");
            sb.Append("window.open('','_self','');");
            sb.Append("</script>");
            RegistClientScript(P, "open", sb.ToString());
        }
    }
}
