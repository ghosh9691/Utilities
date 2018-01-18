using System;
using System.Collections.Generic;
using System.Text;
using Flitesys.GeographicLib;

namespace Flitesys.Utilities
{
    public class GeographicPoint
    {
        public double Latitude;
        public double Longitude;

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
                Distance = g.Distance,
                InitialCourse = g.InitialAzimuth < 0 ? 360.0 + g.InitialAzimuth : g.InitialAzimuth,
                FinalCourse = g.FinalAzimuth < 0 ? 360.0 + g.FinalAzimuth : g.FinalAzimuth
            };
        }

        public GeographicPoint DestinationPoint(double course, double distance)
        {
            GeodesicData g = Geodesic.WGS84.Direct(this.Latitude, this.Longitude, course, distance);
            return new GeographicPoint
            {
                Latitude = g.Latitude2,
                Longitude = g.Longitude2
            };
        }
    }
}