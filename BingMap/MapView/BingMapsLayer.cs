using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace BingMap.MapView
{
	public class BingMapsLayer : Layer
	{
		public readonly static int A_LAYER_TYPE;

		public readonly static int R_LAYER_TYPE;

		public readonly static int H_LAYER_TYPE;

		public readonly static string ALayerUrlFront;

		public readonly static string RLayerUrlFront;

		public readonly static string HLayerUrlFront;

		public readonly static string LayerAUrlEnd;

		private readonly int TileSize = 256;

		private Hashtable m_MapHash = new Hashtable();

		private Hashtable m_ThreadPool = new Hashtable();

		private int m_MapLayerType;

		private int m_LeftUpTileX;

		private int m_LeftUpTileY;

		private int m_EndTileX;

		private int m_EndTileY;

		static BingMapsLayer()
		{
			BingMapsLayer.A_LAYER_TYPE = 50;
			BingMapsLayer.R_LAYER_TYPE = 51;
			BingMapsLayer.H_LAYER_TYPE = 52;
			BingMapsLayer.ALayerUrlFront = "https://ecn.t1.tiles.virtualearth.net/tiles/a";
			BingMapsLayer.RLayerUrlFront = "https://ecn.t1.tiles.virtualearth.net/tiles/r";
			BingMapsLayer.HLayerUrlFront = "https://ecn.t1.tiles.virtualearth.net/tiles/h";
			BingMapsLayer.LayerAUrlEnd = ".jpeg?g=3649";
		}

		public BingMapsLayer(int maxW, int maxH, MapView view) : base(maxW, maxH, view)
		{
			this.m_ThreadPool.Clear();
			this.m_MapHash.Clear();
			this.m_MapLayerType = BingMapsLayer.A_LAYER_TYPE;
			this.m_LeftUpTileX = 0;
			this.m_LeftUpTileY = 0;
            this.m_DrawWidth = this.TileSize * this.MapView.Level * 2;
            this.m_DrawHeight = this.TileSize * this.MapView.Level * 2;
        }

		public void ChangeLayer(int layerType)
		{
			this.m_MapLayerType = layerType;
			this.m_MapHash.Clear();
			this.StopLoadMapThread();
			this.LoadFile();
		}

		public override void Close()
		{
			this.StopLoadMapThread();
			this.m_MapHash.Clear();
			base.Close();
		}

		public void GetLatitudeAndLongitude(int x, int y, out double latitude, out double longitude)
		{
			latitude = 0;
			longitude = 0;
			int pixelX = x + this.m_LeftUpPixelX;
			int pixelY = y + this.m_LeftUpPixelY;
            TileSystem.PixelXYToLatLong(pixelX, pixelY, this.MapView.Level, out latitude, out longitude);
        }

		public void LoadFile(int LayerType)
		{
			this.m_MapLayerType = LayerType;
			this.LoadFile();
		}

		public override void LoadFile()
		{
            if (this.MapView.Level != 1)
            {
                TileSystem.PixelXYToTileXY(this.m_LeftUpPixelX, this.m_LeftUpPixelY, out this.m_LeftUpTileX, out this.m_LeftUpTileY);
                int endX = this.m_LeftUpPixelX + this.m_MaxWidth;
                endX = ((double)endX < Math.Pow(2, (double)this.MapView.Level) * (double)this.TileSize ? endX : (int)Math.Pow(2, (double)this.MapView.Level) * this.TileSize - 1);
                int endY = this.m_LeftUpPixelY + this.m_MaxHeight;
                endY = ((double)endY < Math.Pow(2, (double)this.MapView.Level) * (double)this.TileSize ? endY : (int)Math.Pow(2, (double)this.MapView.Level) * this.TileSize - 1);
                TileSystem.PixelXYToTileXY(endX, endY, out this.m_EndTileX, out this.m_EndTileY);
                //this.MapView.m_ViewWidth = endX - this.m_LeftUpPixelX;
                //this.MapView.m_ViewHeight = endY - this.m_LeftUpPixelY;
                //this.m_DrawWidth = this.MapView.m_ViewWidth;
                //this.m_DrawHeight = this.MapView.m_ViewHeight;
            }
            else
            {
                this.m_LeftUpTileX = 0;
                this.m_LeftUpTileY = 0;
                this.m_EndTileX = 1;
                this.m_EndTileY = 1;
                //this.MapView.m_ViewWidth = (int)Math.Pow(2, (double)this.MapView.m_Level) * this.TileSize;
                //this.MapView.m_ViewHeight = (int)Math.Pow(2, (double)this.MapView.m_Level) * this.TileSize;
            }
            for (int j = this.m_LeftUpTileY; j <= this.m_EndTileY; j++)
            {
                for (int i = this.m_LeftUpTileX; i <= this.m_EndTileX; i++)
                {
                    if (!this.m_MapHash.ContainsKey(string.Concat(i, ",", j)))
                    {
                        BingMapsLayer.LoadMapUrlThread threadObj = new BingMapsLayer.LoadMapUrlThread(this, i, j, this.MapView.Level, this.m_MapLayerType);
                        Thread thread = new Thread(new ThreadStart(threadObj.runThread));
                        if (this.m_ThreadPool.ContainsKey(string.Concat(i, ",", j)))
                        {
                            this.m_ThreadPool[string.Concat(i, ",", j)] = thread;
                        }
                        else
                        {
                            this.m_ThreadPool.Add(string.Concat(i, ",", j), thread);
                        }
                        thread.Start();
                    }
                }
            }
        }

        public new void Paint(Graphics graphics)
        {
            graphics.Clear(Color.Black);
            TileSystem.TileXYToPixelXY(this.m_LeftUpTileX, this.m_LeftUpTileY, out int tileX, out int tileY);
            int offsetX = tileX - this.m_LeftUpPixelX;
            int offsetY = tileY - this.m_LeftUpPixelY;
            int pointX = 0;
            int pointY = 0;
            for (int i = this.m_LeftUpTileY; i <= this.m_EndTileY; i++)
            {
                for (int j = this.m_LeftUpTileX; j <= this.m_EndTileX; j++)
                {
                    if (this.m_MapHash.ContainsKey(string.Concat(j, ",", i)))
                    {
                        pointX = j - this.m_LeftUpTileX;
                        pointY = i - this.m_LeftUpTileY;
                        Image mapImage = (Image)this.m_MapHash[string.Concat(j, ",", i)];
                        graphics.DrawImage(mapImage, new Point(offsetX + pointX * this.TileSize, offsetY + pointY * this.TileSize));
                    }
                }
            }
        }

		private void StopLoadMapThread()
		{
			//if (this.m_ThreadPool.Count > 0)
			//{
			//	foreach (Thread thread in this.m_ThreadPool.Values)
			//	{
			//		thread.Interrupt();
			//	}
			//	this.m_ThreadPool.Clear();
			//}
		}

		public override void ViewAllMapAndCentor()
		{
            base.ViewAllMapAndCentor();
            this.m_LeftUpTileX = 0;
            this.m_LeftUpTileY = 0;
            this.m_DrawWidth = this.TileSize * this.MapView.Level * 2;
            this.m_DrawHeight = this.TileSize * this.MapView.Level * 2;
            this.m_MapHash.Clear();
            this.StopLoadMapThread();
			this.MapView.Invalidate();
        }

		public override void ZoomIn(int x, int y)
		{
            int pixelX = x + this.m_LeftUpPixelX;
            int pixelY = y + this.m_LeftUpPixelY;
            int level = this.MapView.Level;
            double latitude = 0;
            double longitude = 0;
            TileSystem.PixelXYToLatLong(pixelX, pixelY, level - 1, out latitude, out longitude);
            TileSystem.LatLongToPixelXY(latitude, longitude, level, out pixelX, out pixelY);
            pixelX = pixelX - this.m_MaxWidth / 2;
            pixelY = pixelY - this.m_MaxHeight / 2;
            this.m_LeftUpPixelX = pixelX;
            this.m_LeftUpPixelY = pixelY;
            this.m_MapHash.Clear();
            this.StopLoadMapThread();
            this.LoadFile();
        }

		public override void ZoomOut(int x, int y)
		{
            int level = this.MapView.Level;
            int pixelX = x + this.m_LeftUpPixelX;
            int pixelY = y + this.m_LeftUpPixelY;
            double latitude = 0;
            double longitude = 0;
            TileSystem.PixelXYToLatLong(pixelX, pixelY, level + 1, out latitude, out longitude);
            TileSystem.LatLongToPixelXY(latitude, longitude, level, out pixelX, out pixelY);
            pixelX = pixelX - this.m_MaxWidth / 2;
            pixelY = pixelY - this.m_MaxHeight / 2;
            this.m_LeftUpPixelX = pixelX;
            this.m_LeftUpPixelY = pixelY;
            this.m_MapHash.Clear();
            this.StopLoadMapThread();
            this.LoadFile();
        }

		private class LoadMapUrlThread
		{
			private int m_TileX;

			private int m_TileY;

			private int m_Level;

			private int m_Mode;

			public bool m_isOldThread;

			private BingMapsLayer m_Layer;

			public LoadMapUrlThread(BingMapsLayer layer, int tileX, int tileY, int level, int mode)
			{
				this.m_Layer = layer;
				this.m_TileX = tileX;
				this.m_TileY = tileY;
				this.m_Level = level;
				this.m_Mode = mode;
				this.m_isOldThread = false;
			}

			public void runThread()
			{
				string urlFront;
				try
				{
					WebClient wc = new WebClient();
					ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
					if (this.m_Mode != BingMapsLayer.H_LAYER_TYPE)
					{
						urlFront = (this.m_Mode != BingMapsLayer.R_LAYER_TYPE ? BingMapsLayer.ALayerUrlFront : BingMapsLayer.RLayerUrlFront);
					}
					else
					{
						urlFront = BingMapsLayer.HLayerUrlFront;
					}
					if (!this.m_Layer.m_MapHash.ContainsKey(string.Concat(this.m_TileX, ",", this.m_TileY)))
					{
						string quadkey = TileSystem.TileXYToQuadKey(this.m_TileX, this.m_TileY, this.m_Level);
						try
						{
							Image image = Image.FromStream(new MemoryStream(wc.DownloadData(string.Concat(urlFront, quadkey, BingMapsLayer.LayerAUrlEnd))));
							if ((this.m_Layer.m_MapHash.ContainsKey(string.Concat(this.m_TileX, ",", this.m_TileY)) ? false : !this.m_isOldThread))
							{
								this.m_Layer.m_MapHash.Add(string.Concat(this.m_TileX, ",", this.m_TileY), image);
								//Console.WriteLine(string.Concat(new object[] { "Tile (", this.m_TileX, ",", this.m_TileY, ") Load End" }));
							}
						}
						catch (Exception exception)
						{
							Console.WriteLine(exception.StackTrace);
						}
					}
					this.m_Layer.m_ThreadPool.Remove(string.Concat(this.m_TileX, ",", this.m_TileY));
					this.m_Layer.UpdateLayer();
				}
				catch (ThreadInterruptedException threadInterruptedException)
				{
					Console.WriteLine(string.Concat(new object[] { "Interrupted Load Tile (", this.m_TileX, ",", this.m_TileY, ")" }));
				}
			}
		}
	}
}
