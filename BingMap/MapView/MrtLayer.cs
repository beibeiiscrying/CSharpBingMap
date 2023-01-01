using BingMap.Model;
using BingMap.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace BingMap.MapView
{
    public class MrtLayer : Layer
    {

		private Hashtable m_DataTable = new Hashtable();

		private EarthPoint m_LayerMinEarthPoint;

		private EarthPoint m_LayerMaxEarthPoint;


		public MrtLayer(int maxW, int maxH, MapView view) : base(maxW, maxH, view)
		{
			this.m_LayerMinEarthPoint = new EarthPoint(999, 999);
			this.m_LayerMaxEarthPoint = new EarthPoint();
		}

		public override void LoadFile()
		{
			base.LoadFile();
			if (string.IsNullOrEmpty(m_RescoresPath)) { return; }
			//string[] GeoLines = Resources.Khsc_mrt.Remove(Resources.Khsc_mrt.Length - 1).Split('\n');
			//string[] CsvLines = Resources.Khsc_mrt_u.Remove(Resources.Khsc_mrt_u.Length - 1).Split('\n');
			string path = $"{m_RescoresPath}/";

			string[] GeoLines = File.ReadAllLines($"{path}{m_RescoresList[0]}");
			string[] CsvLines = File.ReadAllLines($"{path}{m_RescoresList[1]}");
			//string[] GeoLines = File.ReadAllLines("../../Resources/Khsc_mrt.geo");
			//string[] CsvLines = File.ReadAllLines("../../Resources/Khsc_mrt_u.csv");
			for (int i = 0; i < (int)GeoLines.Length; i++)
			{
				string lineString = GeoLines[i];
				string[] subString = lineString.Split(new char[] { ',' });
				MrtLayer.MrtData data = new MrtLayer.MrtData()
				{
					m_Id = int.Parse(subString[0]),
					m_PointNum = int.Parse(subString[1])
				};
				for (int j = 2; j < (int)subString.Length; j++)
				{
					EarthPoint point = new EarthPoint()
					{
						Longitude = double.Parse(subString[j])
					};
					j++;
					point.Latitude = double.Parse(subString[j]);
					if (data.m_MatLinePoints.Count <= 0)
					{
						data.m_MinX = point.Latitude;
						data.m_MaxX = point.Latitude;
						data.m_MinY = point.Longitude;
						data.m_MaxY = point.Longitude;
					}
					else
					{
						if (data.m_MinX > point.Latitude)
						{
							data.m_MinX = point.Latitude;
						}
						else if (data.m_MaxX < point.Latitude)
						{
							data.m_MaxX = point.Latitude;
						}
						if (data.m_MinY > point.Longitude)
						{
							data.m_MinY = point.Longitude;
						}
						else if (data.m_MaxY < point.Longitude)
						{
							data.m_MaxY = point.Longitude;
						}
					}
					data.m_MatLinePoints.Add(point);
				}
				this.m_DataTable.Add(data.m_Id, data);
				if (this.m_LayerMinEarthPoint.Latitude > data.m_MinX)
				{
					this.m_LayerMinEarthPoint.Latitude = data.m_MinX;
				}
				else if (this.m_LayerMaxEarthPoint.Latitude < data.m_MaxX)
				{
					this.m_LayerMaxEarthPoint.Latitude = data.m_MaxX;
				}
				if (this.m_LayerMinEarthPoint.Longitude > data.m_MinY)
				{
					this.m_LayerMinEarthPoint.Longitude = data.m_MinY;
				}
				else if (this.m_LayerMaxEarthPoint.Longitude < data.m_MaxY)
				{
					this.m_LayerMaxEarthPoint.Longitude = data.m_MaxY;
				}
			}
			for (int i = 0; i < (int)CsvLines.Length; i++)
			{
				string lineString = CsvLines[i];
				string[] subString = lineString.Split(new char[] { ',' });
				int Id = int.Parse(subString[0]);
				if (this.m_DataTable.ContainsKey(Id))
				{
					string colorString = subString[3];
					if (colorString.Equals("紅線"))
					{
						((MrtLayer.MrtData)this.m_DataTable[Id]).m_LineColor = Color.Red;
					}
					else if (colorString.Equals("橘線"))
					{
						((MrtLayer.MrtData)this.m_DataTable[Id]).m_LineColor = Color.Orange;
					}
				}
			}
		}

		public override void Paint(Graphics graphics)
		{
			if (graphics == null) { return; }
			int pixelX;
			int pixelY;
			bool flag;
			this.m_LeftUpPixelX = this.MapView.MapsLayer.m_LeftUpPixelX;
			this.m_LeftUpPixelY = this.MapView.MapsLayer.m_LeftUpPixelY;
			if (this.MapView.Level > 9)
			{
				int LayerPixelX = 0;
				int LayerPixelY = 0;
				int MaxLayerPixelX = 0;
				int MaxLayerPixelY = 0;
				TileSystem.LatLongToPixelXY(this.m_LayerMinEarthPoint.Latitude, this.m_LayerMinEarthPoint.Longitude, this.MapView.Level, out LayerPixelX, out LayerPixelY);
				TileSystem.LatLongToPixelXY(this.m_LayerMaxEarthPoint.Latitude, this.m_LayerMaxEarthPoint.Longitude, this.MapView.Level, out MaxLayerPixelX, out MaxLayerPixelY);
				int temp = MaxLayerPixelX;
				MaxLayerPixelX = (MaxLayerPixelX > LayerPixelX ? MaxLayerPixelX : LayerPixelX);
				LayerPixelX = (LayerPixelX < temp ? LayerPixelX : temp);
				temp = MaxLayerPixelY;
				MaxLayerPixelY = (MaxLayerPixelY > LayerPixelY ? MaxLayerPixelY : LayerPixelY);
				LayerPixelY = (LayerPixelY < temp ? LayerPixelY : temp);
				if ((this.m_LeftUpPixelX >= MaxLayerPixelX || this.m_LeftUpPixelX <= LayerPixelX) && (this.m_LeftUpPixelX + this.MapView.Width <= LayerPixelX || this.m_LeftUpPixelX + this.MapView.Width >= MaxLayerPixelX) && (this.m_LeftUpPixelX >= LayerPixelX || this.m_LeftUpPixelX + this.MapView.Width <= MaxLayerPixelX))
				{
					flag = false;
				}
				else if ((this.m_LeftUpPixelY >= MaxLayerPixelY || this.m_LeftUpPixelY <= LayerPixelY) && (this.m_LeftUpPixelY + this.MapView.Height <= LayerPixelY || this.m_LeftUpPixelY + this.MapView.Height >= MaxLayerPixelY))
				{
					flag = (this.m_LeftUpPixelY >= LayerPixelY ? false : this.m_LeftUpPixelY + this.MapView.Height > MaxLayerPixelY);
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					foreach (MrtLayer.MrtData data in this.m_DataTable.Values)
					{
						Point[] LinePoints = new Point[data.m_PointNum];
						for (int j = 0; j < data.m_PointNum; j++)
						{
							TileSystem.LatLongToPixelXY(data.m_MatLinePoints[j].Latitude, data.m_MatLinePoints[j].Longitude, this.MapView.Level, out pixelX, out pixelY);
							pixelX -= this.m_LeftUpPixelX;
							pixelY -= this.m_LeftUpPixelY;
							LinePoints[j] = new Point(pixelX, pixelY);
						}
						graphics.DrawLines(new Pen(data.m_LineColor, 5f), LinePoints);
					}
				}
			}
		}

		private class MrtData
		{
			public int m_Id;

			public int m_PointNum;

			public double m_MinX;

			public double m_MinY;

			public double m_MaxX;

			public double m_MaxY;

			public List<EarthPoint> m_MatLinePoints;

			public Color m_LineColor;

			public MrtData()
			{
				this.m_Id = 0;
				this.m_MinX = 0;
				this.m_MinY = 0;
				this.m_MaxX = 0;
				this.m_MaxY = 0;
				this.m_LineColor = Color.White;
				this.m_MatLinePoints = new List<EarthPoint>();
			}
		}
    }
}
