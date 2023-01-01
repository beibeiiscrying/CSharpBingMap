using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace BingMap
{
    public partial class MapView : UserControl
    {
        private BufferedGraphicsContext m_CurrentContext = new BufferedGraphicsContext();
        private BufferedGraphics m_Graphics;
        private bool m_Isfirst = true;
        List<Image> m_Images = new List<Image>();
        public MapView()
        {
            InitializeComponent();
        }

        private void MapView_Load(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            m_Images.Add(Image.FromStream(new MemoryStream(wc.DownloadData("https://ecn.t1.tiles.virtualearth.net/tiles/a0.jpeg?g=3649"))));
            m_Images.Add(Image.FromStream(new MemoryStream(wc.DownloadData("https://ecn.t1.tiles.virtualearth.net/tiles/a1.jpeg?g=3649"))));
            m_Images.Add(Image.FromStream(new MemoryStream(wc.DownloadData("https://ecn.t1.tiles.virtualearth.net/tiles/a2.jpeg?g=3649"))));
            m_Images.Add(Image.FromStream(new MemoryStream(wc.DownloadData("https://ecn.t1.tiles.virtualearth.net/tiles/a3.jpeg?g=3649"))));

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            m_CurrentContext = BufferedGraphicsManager.Current;
            m_Graphics = m_CurrentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            m_Graphics.Graphics.DrawImage(GetBmp(), new Point(0, 0));
            m_Graphics.Render(this.CreateGraphics());
            //m_Graphics.Graphics.DrawLine(pen, x1, y1, x2, y2);
            m_Graphics.Graphics.Clear(Color.Gray);
        }

        private Bitmap ResizeImage(Bitmap bmp)
        {
            //Get the image current width  
            int sourceWidth = bmp.Width;
            //Get the image current height  
            int sourceHeight = bmp.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)this.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)this.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width  
            int destWidth = (int)(sourceWidth * nPercentW);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercentH);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(bmp, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }

        private Bitmap GetBmp()
        {
            Bitmap newie = new Bitmap(512, 512);
            //Image img = Image.;
            Graphics _g = Graphics.FromImage(newie);
            _g.DrawImage(m_Images[0], new Point(0, 0));
            _g.DrawImage(m_Images[1], new Point(256, 0));
            _g.DrawImage(m_Images[2], new Point(0, 256));
            _g.DrawImage(m_Images[3], new Point(256, 256));
            _g.Dispose();

            return ResizeImage(newie);

            //Graphics g = e.Graphics;
            //m_Graphics.Clear(Color.White);
            //m_Graphics.Graphics.DrawImage(newie, new Point(0, 0));
            
        }
        private void MapView_Paint(object sender, PaintEventArgs e)
        {
            if (!m_Isfirst) { return; }

            Graphics g = e.Graphics;
            g.DrawImage(GetBmp(), new Point(0, 0));
        }

        private void MapView_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
