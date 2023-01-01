using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BingMap.DB;
using BingMap.Manager;
using BingMap.Model;
using System.Runtime.InteropServices;

namespace BingMap
{
    public partial class MainForm : Form
    {

        private List<CheckBox> m_CbLayerList = new List<CheckBox>();
        private int MouseDownX = 0;
        private int MouseDownY = 0;
        private Rectangle selectedRect;
        private Point OldMouseMovePoint;

        public LoginForm loginForm;
        public MainForm(LoginForm loginForm)
        {
            InitializeComponent();

            MapViewer.SetTownAreaLayerResorces("../../Resources", new string[] { "Khsc_town.geo", "Khsc_town_u.csv" });
            MapViewer.SetLandmarkLayerResorces("../../Resources", new string[] { "Khsc_landmark.geo", "Khsc_landmark_u.csv" });
            MapViewer.SetMrtLineLayerResorces("../../Resources", new string[] { "Khsc_mrt.geo", "Khsc_mrt_u.csv" });

            this.loginForm = loginForm;
            BtnZoomRect.Tag = 0;
            m_CbLayerList.Add(CbTown);
            m_CbLayerList.Add(CbMark);
            m_CbLayerList.Add(CbMRTLine);
            bool[] isOpens = { UserManager.Instance.IsOpenTown, UserManager.Instance.IsOpenMark, UserManager.Instance.IsOpenMRTLine };

            foreach (var cb in m_CbLayerList)
            {
                cb.ForeColor = SystemColors.ScrollBar;
                cb.Tag = m_CbLayerList.IndexOf(cb);
                cb.Checked = isOpens[m_CbLayerList.IndexOf(cb)];
                cb.CheckedChanged += CbLayer_CheckedChanged;
            }
            this.MapViewer.ShowTownLayer = CbTown.Checked;
            this.MapViewer.ShowMarkLayer = CbMark.Checked;
            this.MapViewer.ShowMrtLineLayer = CbMRTLine.Checked;

            base.MouseWheel += new MouseEventHandler(this.MapViewer_MouseWheel);
            base.Width = UserManager.Instance.WinWidth;
            base.Height = UserManager.Instance.WinHeight;
            this.SetLayerTypeChange(UserManager.Instance.Type);
            this.OldMouseMovePoint = new Point(0, 0);
        }
        private void SetLayerTypeChange(int type)
        {
            if (type == LayerType.A_LAYER_TYPE)
            {
                this.MapViewer.ChangeLayer(type);
                this.LayerMenuBtnClick(this.ALayerMenuBtn);
            }
            else if (type == LayerType.R_LAYER_TYPE)
            {
                this.MapViewer.ChangeLayer(type);
                this.LayerMenuBtnClick(this.RLayerMenuBtn);
            }
            else if (type == LayerType.H_LAYER_TYPE)
            {
                this.MapViewer.ChangeLayer(type);
                this.LayerMenuBtnClick(this.HLayerMenuBtn);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!UserManager.Instance.IsGuest)
            {
                string setting = string.Concat(new object[] { base.Width, ",", base.Height, ",", this.MapViewer.LayerType, ",", this.CbTown.Checked.ToString(), ",", this.CbMark.Checked.ToString(), ",", this.CbMRTLine.Checked.ToString() });
                //string setting = string.Concat(new object[] { base.Width, ",", base.Height, ",", this.MapViewer.LayerType});
                string viewStatus = string.Concat(new object[] { this.MapViewer.MapsLayer.m_LeftUpPixelX, ",", this.MapViewer.MapsLayer.m_LeftUpPixelY, ",", this.MapViewer.Level });
                DbHelper.UpdateUserSettingAndViewInfo(UserManager.Instance.UserName, setting, viewStatus);
            }
            if (this.loginForm != null)
            {
                UserManager.Instance.initUserInfo();
                this.loginForm.Show();
            }
            loginForm.Show();
        }

        private void MapViewer_MouseDown(object sender, MouseEventArgs e)
        {
            this.MouseDownX = e.Location.X;
            this.MouseDownY = e.Location.Y;
        }

        private void MapViewer_MouseMove(object sender, MouseEventArgs e)
        {
            double latitude = 0;
            double longitude = 0;
            this.MapViewer.GetLatitudeAndLongitude(e.X, e.Y, out latitude, out longitude);
            this.MapStatus.Text = string.Concat(latitude, " , ", longitude);
            if (Math.Abs(this.OldMouseMovePoint.X - e.X) + Math.Abs(this.OldMouseMovePoint.Y - e.Y) > 8)
            {
                //Console.WriteLine(string.Concat(new object[] { "MouseMove X:", e.X, " Y:", e.Y }));
                if (this.CbMark.Checked) {
                    this.MapViewer.ShowMarkInfo(latitude, longitude, this.MarkToolTip);
                }
                this.OldMouseMovePoint.X = e.X;
                this.OldMouseMovePoint.Y = e.Y;
            }
            if (e.Button != MouseButtons.Left) { return; }

            if ((int)BtnZoomRect.Tag == 1) {
                Pen bluePen = new Pen(Color.FromArgb(255, 0, 0, 255), 5f);
                int rectX = (e.X > this.MouseDownX ? this.MouseDownX : e.X);
                int rectY = (e.Y > this.MouseDownY ? this.MouseDownY : e.Y);
                int width = Math.Abs(e.X - this.MouseDownX);
                int height = Math.Abs(e.Y - this.MouseDownY);
                this.selectedRect = new Rectangle(rectX, rectY, width, height);
                this.MapViewer.Invalidate();
            } else { // map move mode
                int offsetX = e.X - this.MouseDownX;
                int offsetY = e.Y - this.MouseDownY;
                //this.mapsView.MoveMap(offsetX, offsetY);
                this.MouseDownX = e.X;
                this.MouseDownY = e.Y;
                //this.MapPictureBox.Invalidate();
                this.MapViewer.MoveMap(offsetX, offsetY);
            }
        }

        private void MapViewer_MouseUp(object sender, MouseEventArgs e)
        {
            if ((int)this.BtnZoomRect.Tag == 1)
            {
                this.BtnZoomRect.Tag = 0;
                int selectCentorX = this.selectedRect.Left + this.selectedRect.Width / 2;
                int selectCentorY = this.selectedRect.Top + this.selectedRect.Height / 2;
                this.MapViewer.ZoomIn(selectCentorX, selectCentorY);
                this.MapViewer.Invalidate();
            }
            this.MouseDownX = 0;
            this.MouseDownY = 0;

        }

        private void BtnZoomIn_Click(object sender, EventArgs e)
        {
            int width = base.Width;
            int height = base.Height;
            this.MapViewer.ZoomIn(width / 2, height / 2);
            this.MapViewer.Invalidate();
            BtnZoomRect.Tag = 0;
        }

        private void BtnZoomOut_Click(object sender, EventArgs e)
        {
            int width = base.Width;
            int height = base.Height;
            this.MapViewer.ZoomOut(width / 2, height / 2);
            this.MapViewer.Invalidate();
            BtnZoomRect.Tag = 0;
        }

        private void BtnCentor_Click(object sender, EventArgs e)
        {
            this.MapViewer.ViewAllMapAndCentor();
            BtnZoomRect.Tag = 0;
        }

        private void BtnZoomRect_Click(object sender, EventArgs e)
        {
            this.MapViewer.Focus();
            BtnZoomRect.Tag = 1;
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            string findString = this.TbFind.Text;
            string[] array = findString.Split(new char[] { ',' });
            EarthPoint findPoint = new EarthPoint()
            {
                Latitude = double.Parse(array[0]),
                Longitude = double.Parse(array[1])
            };
            this.MapViewer.FindByEarthPoint(findPoint);
        }

        private void MapViewer_Paint(object sender, PaintEventArgs e)
        {
            //this.mapView1.Paint(e);
            if ((int)BtnZoomRect.Tag == 1)
            {
                e.Graphics.DrawRectangle(Pens.Red, this.selectedRect);
            }
        }
        private void MapViewer_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                this.MapViewer.ZoomIn(e.X, e.Y);
            }
            else if (e.Delta < 0)
            {
                this.MapViewer.ZoomOut(e.X, e.Y);
            }
            this.MapViewer.Invalidate();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle printRect = e.PageBounds;
            printRect.Width = printRect.Width - 20;
            printRect.Height = printRect.Height - 20;
            double scale = 1;
            scale = (this.MapViewer.Width <= this.MapViewer.Height ? (double)printRect.Height / (double)this.MapViewer.Height : (double)printRect.Width / (double)this.MapViewer.Width);
            scale = (scale < 1 ? scale : 1);
            Rectangle scaleRect = new Rectangle(10, 10, (int)Math.Ceiling((double)this.MapViewer.Width * scale), (int)Math.Ceiling((double)this.MapViewer.Height * scale));
            Bitmap image = new Bitmap(this.MapViewer.Width, this.MapViewer.Height);
            this.MapViewer.DrawToBitmap(image, new Rectangle(0,0, image.Width, image.Height));
            e.Graphics.DrawImage(image, scaleRect);
        }

        private void PrintPreviewMenuBtn_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.ShowDialog();
        }

        private void LayerMenuBtnClick(System.Windows.Forms.ToolStripMenuItem select)
        {
            this.ALayerMenuBtn.Checked = false;
            this.RLayerMenuBtn.Checked = false;
            this.HLayerMenuBtn.Checked = false;
            select.Checked = true;
        }

        private void CbLayer_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox cb = sender as CheckBox;
            switch ((int)cb.Tag)
            {
                case 0:
                    this.MapViewer.ShowTownLayer = cb.Checked;
                    break;
                case 1:
                    this.MapViewer.ShowMarkLayer = cb.Checked;
                    break;
                case 2:
                    this.MapViewer.ShowMrtLineLayer = cb.Checked;
                    break;
            }
            //m_MapViewerFlagList[(int)cb.Tag] = cb.Checked;

            this.MapViewer.ReloadMap();
        }

        private void ALayerMenuBtn_Click(object sender, EventArgs e)
        {
            this.LayerMenuBtnClick(this.ALayerMenuBtn);
            this.MapViewer.ChangeLayer(LayerType.A_LAYER_TYPE);
            this.MapViewer.Invalidate();
            //this.MapPictureBox.Invalidate();
        }
        private void HLayerMenuBtn_Click(object sender, EventArgs e)
        {

            this.LayerMenuBtnClick(this.HLayerMenuBtn);
            this.MapViewer.ChangeLayer(LayerType.H_LAYER_TYPE);
            this.MapViewer.Invalidate();
        }

        private void RLayerMenuBtn_Click(object sender, EventArgs e)
        {

            this.LayerMenuBtnClick(this.RLayerMenuBtn);
            this.MapViewer.ChangeLayer(LayerType.R_LAYER_TYPE);
            this.MapViewer.Invalidate();
        }
    }
}
