using System;
using System.Text;

namespace BingMap.MapView
{
    internal static class TileSystem
    {
        private const double EarthRadius = 6378137;

		private const double MinLatitude = -85.05112878;

		private const double MaxLatitude = 85.05112878;

		private const double MinLongitude = -180;

		private const double MaxLongitude = 180;

		private static double Clip(double n, double minValue, double maxValue)
		{
			double num = Math.Min(Math.Max(n, minValue), maxValue);
			return num;
		}

		public static double GroundResolution(double latitude, int levelOfDetail)
		{
			latitude = TileSystem.Clip(latitude, -85.05112878, 85.05112878);
			double num = Math.Cos(latitude * 3.14159265358979 / 180) * 2 * 3.14159265358979 * 6378137 / (double)((float)TileSystem.MapSize(levelOfDetail));
			return num;
		}

		public static void LatLongToPixelXY(double latitude, double longitude, int levelOfDetail, out int pixelX, out int pixelY)
		{
			latitude = TileSystem.Clip(latitude, -85.05112878, 85.05112878);
			longitude = TileSystem.Clip(longitude, -180, 180);
			double x = (longitude + 180) / 360;
			double sinLatitude = Math.Sin(latitude * 3.14159265358979 / 180);
			double y = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / 12.5663706143592;
			uint mapSize = TileSystem.MapSize(levelOfDetail);
			pixelX = (int)TileSystem.Clip(x * (double)((float)mapSize) + 0.5, 0, (double)((float)(mapSize - 1)));
			pixelY = (int)TileSystem.Clip(y * (double)((float)mapSize) + 0.5, 0, (double)((float)(mapSize - 1)));
		}

		public static double MapScale(double latitude, int levelOfDetail, int screenDpi)
		{
			double num = TileSystem.GroundResolution(latitude, levelOfDetail) * (double)screenDpi / 0.0254;
			return num;
		}

		public static uint MapSize(int levelOfDetail)
		{
			return (uint)(256 << (levelOfDetail & 31));
		}

		public static void PixelXYToLatLong(int pixelX, int pixelY, int levelOfDetail, out double latitude, out double longitude)
		{
			double mapSize = (double)((float)TileSystem.MapSize(levelOfDetail));
			double x = TileSystem.Clip((double)pixelX, 0, mapSize - 1) / mapSize - 0.5;
			double y = 0.5 - TileSystem.Clip((double)pixelY, 0, mapSize - 1) / mapSize;
			latitude = 90 - 360 * Math.Atan(Math.Exp(-y * 2 * 3.14159265358979)) / 3.14159265358979;
			longitude = 360 * x;
		}

		public static void PixelXYToTileXY(int pixelX, int pixelY, out int tileX, out int tileY)
		{
			tileX = pixelX / 256;
			tileY = pixelY / 256;
		}

		public static void QuadKeyToTileXY(string quadKey, out int tileX, out int tileY, out int levelOfDetail)
		{
			int num = 0;
			int num1 = num;
			tileY = num;
			tileX = num1;
			levelOfDetail = quadKey.Length;
			for (int i = levelOfDetail; i > 0; i--)
			{
				int mask = 1 << (i - 1 & 31);
				switch (quadKey[levelOfDetail - i])
				{
					case '0':
					{
						break;
					}
					case '1':
					{
						tileX |= mask;
						break;
					}
					case '2':
					{
						tileY |= mask;
						break;
					}
					case '3':
					{
						tileX |= mask;
						tileY |= mask;
						break;
					}
					default:
					{
						throw new ArgumentException("Invalid QuadKey digit sequence.");
					}
				}
			}
		}

		public static void TileXYToPixelXY(int tileX, int tileY, out int pixelX, out int pixelY)
		{
			pixelX = tileX * 256;
			pixelY = tileY * 256;
		}

		public static string TileXYToQuadKey(int tileX, int tileY, int levelOfDetail)
		{
			StringBuilder quadKey = new StringBuilder();
			for (int i = levelOfDetail; i > 0; i--)
			{
				char digit = '0';
				int mask = 1 << (i - 1 & 31);
				if ((tileX & mask) != 0)
				{
					digit = (char)(digit + 1);
				}
				if ((tileY & mask) != 0)
				{
					digit = (char)(digit + 1);
					digit = (char)(digit + 1);
				}
				quadKey.Append(digit);
			}
			return quadKey.ToString();
		}
    }
}
