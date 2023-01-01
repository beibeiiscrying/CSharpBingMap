using BingMap.Manager;
using BingMap.Model;
using BingMap.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace BingMap.MapView
{
    public partial class MapView : UserControl
    {
        public BingMapsLayer MapsLayer;

        public MrtLayer MrtLineLayer;

        public TownLayer TownAreaLayer;

        public LandmarkLayer LandMarkPointLayer;

        public int OnFormX { get { return m_OnFormX; } set { m_OnFormX = value; } }
        public int OnFormY { get { return m_OnFormY; } set { m_OnFormY = value; } }
        public int Level{ get { return m_Level; } set { m_Level = value; } }
        public int LayerType{ get { return m_LayerType; } set { m_LayerType = value; } }
        public bool ShowTownLayer{ get { return m_ShowTownLayer; } set { m_ShowTownLayer = value; } }
        public bool ShowMrtLineLayer{ get { return m_ShowMrtLineLayer; } set { m_ShowMrtLineLayer = value; } }
        public bool ShowMarkLayer{ get { return m_ShowMarkLayer; } set { m_ShowMarkLayer = value; } }
        public string TownLayerRescoresPath { get { return TownAreaLayer.RescoresPath; } set { TownAreaLayer.RescoresPath = value; } }
        public string LandMarkLayerRescoresPath { get { return LandMarkPointLayer.RescoresPath; } set { LandMarkPointLayer.RescoresPath = value; } }
        public string MrtLayerRescoresPath { get { return MrtLineLayer.RescoresPath; } set { MrtLineLayer.RescoresPath = value; } }

        private int m_OnFormX;
        private int m_OnFormY;
        private int m_Level;
        private int m_LayerType;
        private bool m_UseFindPoint = false;
        private bool m_ShowTownLayer = true;
        private bool m_ShowMarkLayer = true;
        private bool m_ShowMrtLineLayer = true;
        private EarthPoint m_FindPoint = new EarthPoint();

        //private BufferedGraphicsContext m_CurrentContext = new BufferedGraphicsContext();
        //private BufferedGraphics m_Graphics;

        private readonly List<Action<Graphics>> m_PaintActions = new List<Action<Graphics>>();

        List<Image> m_Images = new List<Image>();
        public MapView()
        {
            InitializeComponent();

            m_Level = 1;
            MapsLayer = new BingMapsLayer(Width, Height, this);
            MrtLineLayer = new MrtLayer(Width, Height, this);
            TownAreaLayer = new TownLayer(Width, Height, this);
            LandMarkPointLayer = new LandmarkLayer(Width, Height, this);
            LoadUserData();
            MapsLayer.LoadFile(m_LayerType);
            this.MrtLineLayer.LoadFile();
            this.TownAreaLayer.LoadFile();
            this.LandMarkPointLayer.LoadFile();
        }

        private void MapView_Load(object sender, EventArgs e)
        {
            TownAreaLayer.LoadFile();
            LandMarkPointLayer.LoadFile();
            MrtLineLayer.LoadFile();
            m_PaintActions.Add(TownAreaLayer.Paint);
            m_PaintActions.Add(LandMarkPointLayer.Paint);
            m_PaintActions.Add(MrtLineLayer.Paint);
        }


        public void SetTownAreaLayerResorces(string path, string[] fileNames)
        {
            TownLayerRescoresPath = path;
            TownAreaLayer.Rescores = fileNames;
        }
        public void SetLandmarkLayerResorces(string path, string[] fileNames)
        {
            LandMarkLayerRescoresPath = path;
            LandMarkPointLayer.Rescores = fileNames;
        }
        public void SetMrtLineLayerResorces(string path, string[] fileNames)
        {
            MrtLayerRescoresPath = path;
            MrtLineLayer.Rescores = fileNames;
        }

        public void ViewAllMapAndCentor()
        {
            UserManager.Instance.initUserInfo();
            int tempType = this.m_LayerType;
            this.LoadUserData();
            this.m_LayerType = tempType;
            this.MapsLayer.LoadFile(this.m_LayerType);
        }

        public void ShowMarkInfo(double latitude, double longitude, ToolTip MarkToolTip)
        {
            this.LandMarkPointLayer.ShowLandmarkInfo(latitude, longitude, MarkToolTip);
        }

        private void CenterLayer(Layer layer)
		{
			if (this.Width <= layer.m_DrawWidth)
			{
				this.m_OnFormX = 0;
			}
			else
			{
				this.m_OnFormX = (this.Width - layer.m_DrawWidth) * 2 / 5;
			}
			if (this.Height <= layer.m_DrawHeight)
			{
				this.m_OnFormY = 0;
			}
			else
			{
				this.m_OnFormY = (this.Height - layer.m_DrawHeight) * 2 / 5;
			}
		}

        private void MapView_Paint(object sender, PaintEventArgs e)
        {
            int findPixelX;
            int findPixelY;
            //m_CurrentContext = BufferedGraphicsManager.Current;
            //m_Graphics = m_CurrentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);

            bool[] flags = { m_ShowTownLayer, m_ShowMarkLayer, m_ShowMrtLineLayer};

            Bitmap tempImage = new Bitmap(Width, Height);
            Graphics graphics = Graphics.FromImage(tempImage);
            this.MapsLayer.Paint(graphics);

            int flagIdx = 0;
            foreach (var foo in m_PaintActions) {
                foo( flags[flagIdx++] ? graphics : null );
            }

            if (this.m_UseFindPoint)
            {
                TileSystem.LatLongToPixelXY(this.m_FindPoint.Latitude, this.m_FindPoint.Longitude, this.m_Level, out findPixelX, out findPixelY);
                findPixelX = findPixelX - this.MapsLayer.m_LeftUpPixelX - 22;
                findPixelY = findPixelY - this.MapsLayer.m_LeftUpPixelY - 80;
                graphics.DrawImage(Resources.locate, findPixelX, findPixelY);
            }
            e.Graphics.DrawImage(tempImage, 0, 0);
        }
        public void ReloadMap()
        {
            this.MapsLayer.UpdateLayer();
        }

        private void MapView_Resize(object sender, EventArgs e)
        {
            if (this.MapsLayer == null) {
                Invalidate();
                return;
            }
            this.MapsLayer.Resize(Width, Height);
            this.MapsLayer.LoadFile();
            this.CenterLayer(this.MapsLayer);
            this.MapsLayer.UpdateLayer();
            this.MrtLineLayer.Resize(Width, Height);
            this.TownAreaLayer.Resize(Width, Height);
            this.LandMarkPointLayer.Resize(Width, Height);
            Invalidate();
        }

        public void FindByEarthPoint(EarthPoint point)
        {
            int pixelX;
            int pixelY;
            this.m_UseFindPoint = true;
            this.m_FindPoint = point;
            TileSystem.LatLongToPixelXY(point.Latitude, point.Longitude, this.m_Level, out pixelX, out pixelY);
            int findLUX = pixelX - this.MapsLayer.m_DrawWidth / 2;
            int findLUY = pixelY - this.MapsLayer.m_DrawHeight / 2;
            int offsetX = findLUX - this.MapsLayer.m_LeftUpPixelX;
            int offsetY = findLUY - this.MapsLayer.m_LeftUpPixelY;
            this.MapsLayer.Move(-offsetX, -offsetY);
            this.MapsLayer.UpdateLayer();
        }

        public void GetLatitudeAndLongitude(int x, int y, out double latitude, out double longitude)
        {
            this.MapsLayer.GetLatitudeAndLongitude(x, y, out latitude, out longitude);
        }

        public void LoadUserData()
        {
            this.m_Level = UserManager.Instance.Level;
            this.m_LayerType = UserManager.Instance.Type;
            this.MapsLayer.setLeftUpPixel(UserManager.Instance.PiexlX, UserManager.Instance.PiexlY);
            this.MrtLineLayer.setLeftUpPixel(UserManager.Instance.PiexlX, UserManager.Instance.PiexlY);
            this.TownAreaLayer.setLeftUpPixel(UserManager.Instance.PiexlX, UserManager.Instance.PiexlY);
            this.LandMarkPointLayer.setLeftUpPixel(UserManager.Instance.PiexlX, UserManager.Instance.PiexlY);
        }

        public void MoveMap(int offsetX, int offsetY)
        {
            this.m_OnFormX += offsetX;
            this.m_OnFormY += offsetY;
            this.MapsLayer.Move(offsetX, offsetY);
            this.Invalidate();
        }

        public void ZoomIn(int x, int y)
        {
            if (m_Level >= 19) { return; }
            this.m_Level++;
            this.MapsLayer.ZoomIn(x, y);
            this.CenterLayer(this.MapsLayer);
        }

        public void ZoomOut(int x, int y)
        {
            if (this.m_Level > 1)
            {
                this.m_Level--;
                this.MapsLayer.ZoomOut(x, y);
                if (this.m_Level == 1)
                {
                    this.CenterLayer(this.MapsLayer);
                    this.MapsLayer.setLeftUpPixel(-this.m_OnFormX, -this.m_OnFormY);
                    this.MapsLayer.UpdateLayer();
                }
            }
        }

        public void ChangeLayer(int layerType)
        {
            this.MapsLayer.ChangeLayer(layerType);
        }
        public void Close()
        {
            this.MapsLayer.Close();
        }
    }
}
