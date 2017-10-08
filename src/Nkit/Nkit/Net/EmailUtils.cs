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
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace Nkit.Net
{
    /// <summary>
    /// 邮件帮助类
    /// </summary>
    public class EmailUtils
    {
        /// <summary>
        /// 邮件主题
        /// </summary>
        public MailMessage MM { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string uid { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string pwd { get; set; }
        /// <summary>
        /// 主机地址(别名或IP地址)
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int port { get; set; }
        /// <summary>
        /// 邮件发送函数
        /// </summary>
        /// <returns>成功|失败</returns>
        public Boolean sendMail()
        {
            SmtpClient client = new SmtpClient();
            //设置邮箱身份验证
            client.Credentials = new System.Net.NetworkCredential(uid, pwd);
            client.Port = port;
            client.Host = host;
            client.EnableSsl = true;
            object userState = MM;
            try
            {
                client.Send(MM);
                return true;
            }
            catch
            {
                return false;
            } 
        }
    }
}
