namespace BingMap.Model
{
    public class EarthPoint
    {
        public double Latitude;

		public double Longitude;

		public EarthPoint()
		{
			this.Latitude = 0;
			this.Longitude = 0;
		}

		public EarthPoint(double latitude, double longitude)
		{
			this.Latitude = latitude;
			this.Longitude = longitude;
		}
    }
}
