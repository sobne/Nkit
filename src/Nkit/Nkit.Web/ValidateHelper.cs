﻿/* License
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
using System.Drawing;
using System.Drawing.Imaging;

namespace Nkit.Web
{
    /// <summary>
    /// Web验证类
    /// </summary>
    public class ValidateHelper
    {
        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="checkCode">随机码</param>
        public static void CreateCheckCodeImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;

            Bitmap image = new Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器
                Random random = new Random();

                //清空图片背景色
                g.Clear(Color.White);

                //画图片的背景噪音线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Gif";
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
       
    }
}
