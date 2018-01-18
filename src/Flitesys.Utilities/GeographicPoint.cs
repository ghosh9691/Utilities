using System;
using System.Collections.Generic;
using System.Text;
using Flitesys.GeographicLib;

namespace Flitesys.Utilities
{
	public class GeographicPoint
	{
		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public GeographicPoint()
		{ }

		public GeographicPoint(double lat, double lon)
		{
			Latitude = lat;
			Longitude = lon;
		}

		public GeographicPoint(GeographicPoint another)
		{
			Latitude = another.Latitude;
			Longitude = another.Longitude;
		}

		/// <summary>
		/// Computes the distance, initial course and final course from this point to another point
		/// </summary>
		/// <param name="toPoint">Destination geographic coordinates</param>
		/// <returns>A GeographicResult object giving the distance, initial course and final course</returns>
		public GeographicResult DistanceTo(GeographicPoint toPoint)
		{
			GeodesicData g = Geodesic.WGS84.Inverse(this.Latitude, this.Longitude, toPoint.Latitude, toPoint.Longitude, GeodesicMask.ALL);
			return new GeographicResult
			{
				Distance = g.s12,
				InitialCourse = g.azi1 < 0 ? 360.0 + g.azi1 : g.azi1,
				FinalCourse = g.azi2 < 0 ? 360.0 + g.azi2 : g.azi2
			};
		}
	}
}
