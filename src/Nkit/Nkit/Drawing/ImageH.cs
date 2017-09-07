using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Nkit.Drawing
{
    public class ImageH
    {
        private static Bitmap DrawBitmap(string sFile, Rectangle destRect, Rectangle srcRect)
        {
            Image iSource = Image.FromFile(sFile);

            Bitmap ob = new Bitmap(destRect.Width, destRect.Height);
            Graphics g = Graphics.FromImage(ob);
            //g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(iSource, destRect, srcRect, GraphicsUnit.Pixel);
            g.Dispose();
            iSource.Dispose();
            return ob;
            
        }
        public static bool GetThumbnail(string sFile, string dFile, int dHeight, int dWidth)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            int sW = 0, sH = 0;

            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Height * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Rectangle destRect = new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH);
            Rectangle srcRect = new Rectangle(0, 0, iSource.Width, iSource.Height);
            Bitmap ob = DrawBitmap(sFile, destRect, srcRect);

            try
            {
                ob.Save(dFile, tFormat);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();

            }
        }

        public static bool GetThumbnail(string sFile, string dFile, double percent, int flag)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            int sW = 0, sH = 0;
            sW = int.Parse(Math.Round(iSource.Width * percent).ToString());
            sH = int.Parse(Math.Round(iSource.Height * percent).ToString());

            Rectangle destRect = new Rectangle(0, 0, sW, sH);
            Rectangle srcRect = new Rectangle(0, 0, iSource.Width, iSource.Height);
            Bitmap ob = DrawBitmap(sFile, destRect, srcRect);


            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();

                ImageCodecInfo jpegICIinfo = null;

                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();

            }
        }
        public static string CutImage(string sFile, string dFile, int X, int Y, int W, int H)
        {
            if ((X < W) && (Y < H))
            {
                Bitmap bitmap = new Bitmap(sFile);
                if (((X + W) <= bitmap.Width) && ((Y + H) <= bitmap.Height))
                {
                    Bitmap destBitmap = new Bitmap(W, H);
                    Rectangle destRect = new Rectangle(0, 0, W, H);
                    Rectangle srcRect = new Rectangle(X, Y, W, H);
                    Bitmap ob = DrawBitmap(sFile, destRect, srcRect);

                    Graphics g = Graphics.FromImage(destBitmap);
                    g.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);

                    System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
                    ImageFormat tFormat = iSource.RawFormat;

                    destBitmap.Save(dFile, tFormat);
                    return dFile;
                }
                else
                {
                    return "截取范围超出图片范围";
                }
            }
            else
            {
                return "请确认(X < W)&&(Y < H)";
            }
        }
    }
}
