using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Lottery.UserControls
{
    public partial class PaintControl : UserControl
    {
        public enum TernaryRasterOperations : uint
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            SRCERASE = 0x00440328,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            MERGECOPY = 0x00C000CA,
            MERGEPAINT = 0x00BB0226,
            PATCOPY = 0x00F00021,
            PATPAINT = 0x00FB0A09,
            PATINVERT = 0x005A0049,
            DSTINVERT = 0x00550009,
            BLACKNESS = 0x00000042,
            WHITENESS = 0x00FF0062,
            CAPTUREBLT = 0x40000000 //only if WinVer >= 5.0.0 (see wingdi.h)
        }
        [DllImport("gdi32.dll")]
        private static extern int BitBlt(
        IntPtr hdcDest,     // handle to destination DC (device context)
        int nXDest,         // x-coord of destination upper-left corner
        int nYDest,         // y-coord of destination upper-left corner
        int nWidth,         // width of destination rectangle
        int nHeight,        // height of destination rectangle
        IntPtr hdcSrc,      // handle to source DC
        int nXSrc,          // x-coordinate of source upper-left corner
        int nYSrc,          // y-coordinate of source upper-left corner
        System.Int32 dwRop  // raster operation code
        );
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern void DeleteObject(IntPtr obj);
        [DllImport("gdi32", EntryPoint = "StretchBlt")]
        public static extern int StretchBlt(
              IntPtr hdc,
              int x,
              int y,
              int nWidth,
              int nHeight,
              IntPtr hSrcDC,
              int xSrc,
              int ySrc,
              int nSrcWidth,
              int nSrcHeight,
              int dwRop
        );
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);



        public PaintControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            ImageBorderColor = Color.White;
            TextHeight = 30;
        }
        public PaintControl(Size itemSize, List<Point> pPoints) :this()
        {
            _ItemSize = itemSize;
            _Points = pPoints;
        }
        protected List<Point> _Points;
        protected Size _ItemSize;
        protected readonly object lockObj = new object();
        

        [Browsable(true), Description("图像边界颜色")]
        public Color ImageBorderColor { get; set; }
        [Browsable(true), Description("字体高度，文字居中，包含文字及上下Margin高度")]
        public int TextHeight { get; set; }


        private void drawImage(Graphics g, Image dst, Image src, Rectangle dstRect)
        {
            IntPtr destDC = g.GetHdc();
            //IntPtr destCDC = CreateCompatibleDC(destDC);
            // IntPtr oldDest = SelectObject(destCDC, IntPtr.Zero);
            Graphics sourceGraphics = Graphics.FromImage(src);
            IntPtr sourceDC = sourceGraphics.GetHdc();
            IntPtr sourceCDC = CreateCompatibleDC(sourceDC);
            IntPtr sourceHB = ((Bitmap)src).GetHbitmap();
            IntPtr oldSource = SelectObject(sourceCDC, sourceHB);
            int success = BitBlt(destDC, dstRect.X, dstRect.Y, dstRect.Width, dstRect.Height,
                sourceCDC, 0, 0, (int)TernaryRasterOperations.SRCPAINT);

            // SelectObject(destCDC, oldDest);
            //  SelectObject(sourceCDC, oldSource);

            //DeleteObject(destCDC);
            DeleteObject(sourceCDC);

            DeleteObject(sourceHB);

            g.ReleaseHdc();
            sourceGraphics.ReleaseHdc();
        }
        protected virtual Bitmap drawImage()
        {
            return null;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // DateTime time1, time2;
            // time1 = DateTime.Now;
            Image img = drawImage();
            e.Graphics.DrawImage(img, 0, 0);
            // time2 = DateTime.Now;
            // Console.WriteLine((time2 - time1).TotalMilliseconds+"ms");
        }
    }
}
