using System.Numerics;
using GeoTimeZone;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PyxisInt.GeographicLib;

namespace PrabalGhosh.Utilities.Geographic
{
    /// <summary>
    /// GeographicPoint is a representation of a location on the surface of the Earth.
    /// This representation can be instantiated from a latitude/longitude value as a double
    /// or from a Point() data type (from NETTopologySuite). 
    /// </summary>
    public class GeographicPoint
    {
        public double Latitude;
        public double Longitude;

        public GeographicPoint(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public GeographicPoint()
        {
        }

        public GeographicPoint(GeographicPoint another)
        {
            Latitude = another.Latitude;
            Longitude = another.Longitude;
        }

        public GeographicPoint(Point point)
        {
            Latitude = point.Coordinate.Y;
            Longitude = point.Coordinate.X;
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

        /// <summary>
        /// Computes the interception point from a location to a great-circle track defined
        /// by the GeographicLine. The returned data contains the distance to the interception
        /// point, the initial and final courses, as well as the point itself.
        /// </summary>
        /// <param name="geoLine"></param>
        /// <returns></returns>
        public GeographicResult Intercept(GeographicLine geoLine)
        {
            var geod = Geodesic.WGS84;
            var gn = new Gnomonic(geod);
            //initial guess...
            var gc = geoLine.GreatCircle();
            var dist = gc.Distance / 2;
            var initialGuess = geoLine.Start.DestinationPoint(gc.InitialCourse, dist);
            double latI = initialGuess.Latitude;
            double lonI = initialGuess.Longitude;
            ConsoleEx.WriteMessage($"Point Intercept: Initial Guess: {latI}/{lonI}");
            for (int i = 0; i < 10; i++)
            {
                var xa1 = gn.Forward(latI, lonI, geoLine.Start.Latitude, geoLine.Start.Longitude);
                var xa2 = gn.Forward(latI, lonI, geoLine.End.Latitude, geoLine.End.Longitude);
                var xb1 = gn.Forward(latI, lonI, this.Latitude, this.Longitude);
                var va1 = new Vector(xa1.x, xa1.y);
                var va2 = new Vector(xa2.x, xa2.y);
                var la = va1.Cross(va2);
                var vb1 = new Vector(xb1.x, xb1.y);
                var lb = new Vector(la.Y, -la.X, la.X * xb1.y - la.Y * xb1.x);
                var p0 = la.Cross(lb);
                p0.Norm();
                var solved = gn.Reverse(latI, lonI, p0.X, p0.Y);
                latI = solved.PointLatitude;
                lonI = solved.PointLongitude;
            }
            ConsoleEx.WriteMessage($"Final Result: {latI}/{lonI}");
            var g = geod.Inverse(latI, lonI, this.Latitude, this.Longitude);
            return new GeographicResult
            {
                Distance = g.Distance,
                InitialCourse = g.InitialAzimuth < 0 ? 360.0 + g.InitialAzimuth : g.InitialAzimuth,
                FinalCourse = g.FinalAzimuth < 0 ? 360.0 + g.FinalAzimuth : g.FinalAzimuth,
                Coords = new GeographicPoint(latI, lonI)
            };
        }

        /// <summary>
        /// Gets the time zone (IANA) where this point lies
        /// </summary>
        /// <returns>IANA Time zone Id</returns>
        public string GetTimezone()
        {
            return TimeZoneLookup.GetTimeZone(this.Latitude, this.Longitude).Result;
        }

        public Point ToGeometryPoint()
        {
            var gf = NtsGeometryServices.Instance.CreateGeometryFactory(4326);
            return gf.CreatePoint(new Coordinate(this.Longitude, this.Latitude));
        }
    }
}