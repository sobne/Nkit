using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Lottery.Entity;

namespace Lottery.UserControls
{
    public partial class GiftPaintControl : PaintControl
    {
        public GiftPaintControl()
        {
            ImageBorderColor = Color.White;
            TextHeight = 30;
        }
        public GiftPaintControl(Size itemSize, List<Point> pPoints) :base(itemSize, pPoints)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        public List<Gift> Items;
        private delegate void handler(List<Gift> items);

        public void SetItems(IList<Gift> items)
        {
            lock (lockObj)
                Items = new List<Gift>(items);

            if (!InvokeRequired)
            {
                this.Invalidate();
            }
            else
            {
                Invoke(new handler(SetItems), items);
            }
        }
        protected override Bitmap drawImage()
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            // g.Clear(this.BackColor);
            Gift[] items = null;
            lock (lockObj)
            {
                if (Items == null || Items.Count <= 0) items = null;
                else
                    items = Items.ToArray();
            }
            if (items == null) items = new Gift[0];
            var h = _ItemSize.Height;
            var w = _ItemSize.Width;
            for (int i = 0; i < _Points.Count && _Points.Count <= items.Length; i++)
            {
                int y = _Points[i].Y;
                int x = _Points[i].X;

                /*绘制矩形*/
                //Rectangle rect  = new Rectangle(x, y, _ItemSize.Width, _ItemSize.Height);
                //rect.Inflate(1, 4);
                //using (Pen p = new Pen(ImageBorderColor))
                //{
                //    g.DrawRectangle(p, rect);
                //}
                /*绘制图像*/
                Image srcImg = items[i].Photo;

                Rectangle dstRect = new Rectangle(x, y, _ItemSize.Width, _ItemSize.Height - TextHeight);
                //   System.Drawing.Imaging.ImageAttributes attri = new System.Drawing.Imaging.ImageAttributes();  是否膳所这列，给一个矩阵即可

                //drawImage(g, bmp, srcImg, dstRect);
                g.DrawImage(srcImg, dstRect, 0, 0, srcImg.Width, srcImg.Height, GraphicsUnit.Pixel);//, attri);
                                                                                                    //bool r =  BitBlt(dstHdc, dstRect.X, dstRect.Y, dstRect.Width, dstRect.Height, hBitmap,
                                                                                                    //     0, 0, CopyPixelOperation.SourceCopy);
                                                                                                    /*绘制图片*/

                /*绘制文字*/
                string text = items[i].Name;
                RectangleF rectF = new RectangleF(x, y + _ItemSize.Height - TextHeight + 10, _ItemSize.Width, TextHeight);
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.FormatFlags = StringFormatFlags.NoWrap;
                sf.LineAlignment = StringAlignment.Center;
                sf.Trimming = StringTrimming.Character;
                Color fColor = Color.White;
                if (items[i].Level <= 2)
                    fColor = Color.FromArgb(255, 255, 215, 0);

                Font ft = new Font("楷体", 20f, FontStyle.Bold);
                if (items[i].Name.Length > 3) ft = new Font("楷体", 16.5f);
                using (Brush br = new SolidBrush(fColor))
                {
                    g.DrawString(text, ft, br, rectF, sf);
                }
            }
            return bmp;
        }
    }
}
