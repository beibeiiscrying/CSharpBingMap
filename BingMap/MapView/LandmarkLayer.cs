using BingMap.Model;
using BingMap.Properties;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BingMap.MapView
{
    public class LandmarkLayer : Layer
	{
		public readonly static int MARK_TYPE_GAS;

		public readonly static int MARK_TYPE_MRT;

		public readonly static int MARK_TYPE_PARKING;

		public readonly static int MARK_TYPE_SCHOOL;

		private readonly static int ShowMinLevel;

		private readonly static int RangeSize;

		private Hashtable m_DataTable = new Hashtable();

		private EarthPoint m_LayerMinEarthPoint;

		private EarthPoint m_LayerMaxEarthPoint;

		private int TipId;

		static LandmarkLayer()
		{
			LandmarkLayer.MARK_TYPE_GAS = 405;
			LandmarkLayer.MARK_TYPE_MRT = 306;
			LandmarkLayer.MARK_TYPE_PARKING = 303;
			LandmarkLayer.MARK_TYPE_SCHOOL = 203;
			LandmarkLayer.ShowMinLevel = 11;
			LandmarkLayer.RangeSize = 10;
		}

		public LandmarkLayer(int maxW, int maxH, MapView view) : base(maxW, maxH, view)
		{
			this.m_LayerMinEarthPoint = new EarthPoint(999, 999);
			this.m_LayerMaxEarthPoint = new EarthPoint();
			this.TipId = -1;
		}

		public override void LoadFile()
		{
			base.LoadFile();
			if (string.IsNullOrEmpty(m_RescoresPath)) { return; }
			//string[] GeoLines = Resources.Khsc_landmark.Remove(Resources.Khsc_landmark.Length-1).Split('\n');
			//         string[] CsvLines = Resources.Khsc_landmark_u.Remove(Resources.Khsc_landmark.Length - 1).Split('\n');
			string path = $"{m_RescoresPath}/";

			string[] GeoLines = File.ReadAllLines($"{path}{m_RescoresList[0]}");
			string[] CsvLines = File.ReadAllLines($"{path}{m_RescoresList[1]}");
			//string[] GeoLines = File.ReadAllLines("../../Resources/Khsc_landmark.geo");
			//         string[] CsvLines = File.ReadAllLines("../../Resources/Khsc_landmark_u.csv");
			for (int i = 0; i < (int)GeoLines.Length; i++)
			{
				string lineString = GeoLines[i];
				string[] subString = lineString.Split(new char[] { ',' });
				LandmarkLayer.LandmarkData data = new LandmarkLayer.LandmarkData()
				{
					m_Id = int.Parse(subString[0])
				};
				data.m_EPoint.Longitude = double.Parse(subString[1]);
				data.m_EPoint.Latitude = double.Parse(subString[2]);
				this.m_DataTable.Add(data.m_Id, data);
			}
			for (int i = 0; i < (int)CsvLines.Length; i++)
			{
				string lineString = CsvLines[i];
				string[] subString = lineString.Split(new char[] { ',' });
				int Id = int.Parse(subString[0]);
				if (this.m_DataTable.ContainsKey(Id))
				{
					int typeNum = int.Parse(subString[2]);
					if ((typeNum == LandmarkLayer.MARK_TYPE_GAS || typeNum == LandmarkLayer.MARK_TYPE_SCHOOL || typeNum == LandmarkLayer.MARK_TYPE_MRT ? true : typeNum == LandmarkLayer.MARK_TYPE_PARKING))
					{
						LandmarkLayer.LandmarkData data = (LandmarkLayer.LandmarkData)this.m_DataTable[Id];
						data.m_Type = typeNum;
						string info = "";
						for (int j = 3; j < (int)subString.Length; j++)
						{
							info = string.Concat(info, subString[j], "\n");
						}
						data.m_Info = info;
						if (this.m_LayerMinEarthPoint.Latitude > data.m_EPoint.Latitude)
						{
							this.m_LayerMinEarthPoint.Latitude = data.m_EPoint.Latitude;
						}
						else if (this.m_LayerMaxEarthPoint.Latitude < data.m_EPoint.Latitude)
						{
							this.m_LayerMaxEarthPoint.Latitude = data.m_EPoint.Latitude;
						}
						if (this.m_LayerMinEarthPoint.Longitude > data.m_EPoint.Longitude)
						{
							this.m_LayerMinEarthPoint.Longitude = data.m_EPoint.Longitude;
						}
						else if (this.m_LayerMaxEarthPoint.Longitude < data.m_EPoint.Longitude)
						{
							this.m_LayerMaxEarthPoint.Longitude = data.m_EPoint.Longitude;
						}
					}
					else
					{
						this.m_DataTable.Remove(Id);
					}
				}
			}
		}

		public override void Paint(Graphics graphics)
		{
			if (graphics == null) { return; }
            bool flag;
            this.m_LeftUpPixelX = this.MapView.MapsLayer.m_LeftUpPixelX;
			this.m_LeftUpPixelY = this.MapView.MapsLayer.m_LeftUpPixelY;
			if (this.MapView.Level > LandmarkLayer.ShowMinLevel)
			{
                TileSystem.LatLongToPixelXY(this.m_LayerMinEarthPoint.Latitude, this.m_LayerMinEarthPoint.Longitude, this.MapView.Level, out int LayerPixelX, out int LayerPixelY);
                TileSystem.LatLongToPixelXY(this.m_LayerMaxEarthPoint.Latitude, this.m_LayerMaxEarthPoint.Longitude, this.MapView.Level, out int MaxLayerPixelX, out int MaxLayerPixelY);
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
					Bitmap GapIcon = Resources.gas;
					Bitmap SchoolIcon = Resources.school;
					Bitmap MrtIcon = Resources.mrt;
					Bitmap ParkingIcon = Resources.parking;
					foreach (LandmarkLayer.LandmarkData data in this.m_DataTable.Values)
					{
						TileSystem.LatLongToPixelXY(data.m_EPoint.Latitude, data.m_EPoint.Longitude, this.MapView.Level, out int pixelX, out int pixelY);
						pixelX = pixelX - this.m_LeftUpPixelX - 10;
						pixelY = pixelY - this.m_LeftUpPixelY - 10;
						if (data.m_Type == LandmarkLayer.MARK_TYPE_GAS)
						{
							graphics.DrawImage(GapIcon, pixelX, pixelY);
						}
						else if (data.m_Type == LandmarkLayer.MARK_TYPE_MRT)
						{
							graphics.DrawImage(MrtIcon, pixelX, pixelY);
						}
						else if (data.m_Type == LandmarkLayer.MARK_TYPE_PARKING)
						{
							graphics.DrawImage(ParkingIcon, pixelX, pixelY);
						}
						else if (data.m_Type == LandmarkLayer.MARK_TYPE_SCHOOL)
						{
							graphics.DrawImage(SchoolIcon, pixelX, pixelY);
						}
					}
				}
			}
		}

		public void ShowLandmarkInfo(double Latitude, double Longitude, ToolTip MarkToolTip)
		{
			int MoustPixelX;
			int MousePixelY;
			int MarkPixelX;
			int MarkPixelY;
			if (this.MapView.Level > LandmarkLayer.ShowMinLevel)
			{
				TileSystem.LatLongToPixelXY(Latitude, Longitude, this.MapView.Level, out MoustPixelX, out MousePixelY);
				MoustPixelX -= this.m_LeftUpPixelX;
				MousePixelY -= this.m_LeftUpPixelY;
				bool isFindMark = false;
				foreach (LandmarkLayer.LandmarkData data in this.m_DataTable.Values)
				{
					TileSystem.LatLongToPixelXY(data.m_EPoint.Latitude, data.m_EPoint.Longitude, this.MapView.Level, out MarkPixelX, out MarkPixelY);
					MarkPixelX = MarkPixelX - this.m_LeftUpPixelX - LandmarkLayer.RangeSize;
					MarkPixelY = MarkPixelY - this.m_LeftUpPixelY - LandmarkLayer.RangeSize;
					if ((MarkPixelX >= MoustPixelX || MarkPixelX + LandmarkLayer.RangeSize * 2 <= MoustPixelX || MarkPixelY >= MousePixelY || MarkPixelY + LandmarkLayer.RangeSize * 2 <= MousePixelY ? false : this.TipId != data.m_Id))
					{
						isFindMark = true;
						this.TipId = data.m_Id;
						this.MapView.Focus();
						MarkToolTip.Show(data.m_Info, this.MapView.TopLevelControl, MarkPixelX, MarkPixelY);
						break;
					}
				}
				if (!isFindMark)
				{
					MarkToolTip.Hide(this.MapView.TopLevelControl);
					this.TipId = -1;
				}
			}
		}

		private class LandmarkData
		{
			public int m_Id;

			public EarthPoint m_EPoint;

			public int m_Type;

			public string m_Info;

			public LandmarkData()
			{
				this.m_Id = 0;
				this.m_Type = 0;
				this.m_EPoint = new EarthPoint();
				this.m_Info = "";
			}
		}
    }
}
