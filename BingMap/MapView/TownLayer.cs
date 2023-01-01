using BingMap.Model;
using BingMap.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace BingMap.MapView
{
    public class TownLayer : Layer
	{

		private Hashtable m_DataTable = new Hashtable();

		private EarthPoint m_LayerMinEarthPoint;

		private EarthPoint m_LayerMaxEarthPoint;

		//private new string m_RescoresPath = string.Empty;
		//private new List<string> m_RescoresList = new List<string>();

		public TownLayer(int maxW, int maxH, MapView view) : base(maxW, maxH, view)
		{
			this.m_LayerMinEarthPoint = new EarthPoint(999, 999);
			this.m_LayerMaxEarthPoint = new EarthPoint();
		}

		public override void LoadFile()
		{
			TownLayer.TownData data;
			base.LoadFile();
			if(string.IsNullOrEmpty(m_RescoresPath)) { return; }
			//string[] GeoLines = Resources.Khsc_town.Remove(Resources.Khsc_town.Length - 1).Split('\n');
			//string[] CsvLines = Resources.Khsc_town_u.Remove(Resources.Khsc_town_u.Length - 1).Split('\n');
			string path = $"{m_RescoresPath}/";

			string[] GeoLines = File.ReadAllLines($"{path}{m_RescoresList[0]}");
			string[] CsvLines = File.ReadAllLines($"{path}{m_RescoresList[1]}");
			//string[] GeoLines = File.ReadAllLines("../../Resources/Khsc_town.geo");
			//string[] CsvLines = File.ReadAllLines("../../Resources/Khsc_town_u.csv");
			for (int i = 0; i < (int)GeoLines.Length; i++)
			{
				string lineString = GeoLines[i];
				string[] subString = lineString.Split(new char[] { ',' });
				int id = int.Parse(subString[1]);
				data = (!this.m_DataTable.ContainsKey(id) ? new TownLayer.TownData() : (TownLayer.TownData)this.m_DataTable[id]);
				data.m_Id = id;
				int linePointNum = int.Parse(subString[2]);
				data.m_PointNums.Add(linePointNum);
				List<EarthPoint> TownLinePoints = new List<EarthPoint>();
				for (int j = 3; j < (int)subString.Length; j++)
				{
					EarthPoint point = new EarthPoint()
					{
						Longitude = double.Parse(subString[j])
					};
					j++;
					point.Latitude = double.Parse(subString[j]);
					if ((TownLinePoints.Count > 0 ? false : data.m_TownLines.Count <= 0))
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
					TownLinePoints.Add(point);
				}
				data.m_TownLines.Add(TownLinePoints);
				if (!this.m_DataTable.ContainsKey(id))
				{
					this.m_DataTable.Add(data.m_Id, data);
				}
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
					((TownLayer.TownData)this.m_DataTable[Id]).m_TownName = subString[4];
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
			if (this.MapView.Level > 7)
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
					foreach (TownLayer.TownData data in this.m_DataTable.Values)
					{
						SolidBrush solidBrush = new SolidBrush(Color.FromArgb(110, 0, 0, 200));
						GraphicsPath graphicspath = new GraphicsPath();
						Pen pen = new Pen(data.m_LineColor, 2f);
						for (int i = 0; i < data.m_TownLines.Count; i++)
						{
							int linePointNum = data.m_PointNums[i];
							Point[] LinePoints = new Point[linePointNum];
							List<EarthPoint> TownLinePoints = data.m_TownLines[i];
							for (int j = 0; j < linePointNum; j++)
							{
								TileSystem.LatLongToPixelXY(TownLinePoints[j].Latitude, TownLinePoints[j].Longitude, this.MapView.Level, out pixelX, out pixelY);
								pixelX -= this.m_LeftUpPixelX;
								pixelY -= this.m_LeftUpPixelY;
								LinePoints[j] = new Point(pixelX, pixelY);
							}
							graphicspath.AddLines(LinePoints);
						}
						graphics.FillPath(solidBrush, graphicspath);
						graphics.DrawPath(pen, graphicspath);
					}
					foreach (TownLayer.TownData data in this.m_DataTable.Values)
					{
						if (this.MapView.Level > 9)
						{
							int StringSize = Math.Min(10 + (this.MapView.Level - 9) * 2, 15);
							int AreaCentorPixelX = 0;
							int AreaCentorPixelY = 0;
							double centorX = (data.m_MaxX - data.m_MinX) / 2 + data.m_MinX;
							double centorY = (data.m_MaxY - data.m_MinY) / 2 + data.m_MinY;
							TileSystem.LatLongToPixelXY(centorX, centorY, this.MapView.Level, out AreaCentorPixelX, out AreaCentorPixelY);
							AreaCentorPixelX -= this.m_LeftUpPixelX;
							AreaCentorPixelX -= 15;
							AreaCentorPixelY -= this.m_LeftUpPixelY;
							AreaCentorPixelY -= 5;
							SolidBrush drawBrush = new SolidBrush(Color.Snow);
							Font font = new Font("微軟正黑體", (float)StringSize, FontStyle.Bold);
							graphics.DrawString(data.m_TownName, font, drawBrush, (float)AreaCentorPixelX, (float)AreaCentorPixelY);
						}
					}
				}
			}
		}

		private class TownData
		{
			public int m_Id;

			public List<int> m_PointNums;

			public double m_MinX;

			public double m_MinY;

			public double m_MaxX;

			public double m_MaxY;

			public List<List<EarthPoint>> m_TownLines;

			public Color m_LineColor;

			public string m_TownName;

			public TownData()
			{
				this.m_Id = 0;
				this.m_MinX = 0;
				this.m_MinY = 0;
				this.m_MaxX = 0;
				this.m_MaxY = 0;
				this.m_LineColor = Color.Blue;
				this.m_PointNums = new List<int>();
				this.m_TownLines = new List<List<EarthPoint>>();
			}
		}
	}
}
