using System;
using System.Numerics;
using GeoTimeZone;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace PrabalGhosh.Utilities.Geographic
{
    /// <summary>
    /// GeographicPoint is a representation of a location on the surface of the Earth.
    /// This representation can be instantiated from a latitude/longitude value as a double
    /// or from a Point() data type (from NETTopologySuite).
    /// </summary>
    public class GeographicPoint
    {
        // World Geodetic System (WGS) - 84
        // A = 6378137.0 m => Semi-major axis of Earth
        // B = 6356752.314245 m => Semi-minor axis of Earth
        // Inverse Flattening => (1/f) = 298.257223563
        // C = B/A
        private const double C = 0.996647189296812; // complement of ellipticity - WGS84

        public double Latitude;
        public double Longitude;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicPoint"/> class with the specified latitude and longitude.
        /// </summary>
        /// <param name="lat">The latitude of the geographic point.</param>
        /// <param name="lon">The longitude of the geographic point.</param>
        public GeographicPoint(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicPoint"/> class.
        /// </summary>
        public GeographicPoint()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicPoint"/> class by copying another geographic point.
        /// </summary>
        /// <param name="another">The geographic point to copy.</param>
        public GeographicPoint(GeographicPoint another)
        {
            Latitude = another.Latitude;
            Longitude = another.Longitude;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicPoint"/> class from a NetTopologySuite Point.
        /// </summary>
        /// <param name="point">The NetTopologySuite Point to convert.</param>
        public GeographicPoint(Point point)
        {
            Latitude = point.Coordinate.Y;
            Longitude = point.Coordinate.X;
        }

        /// <summary>
        /// Converts the geographic point to a spherical point.
        /// </summary>
        /// <returns>A <see cref="SphericalPoint"/> representing the spherical coordinates.</returns>
        public SphericalPoint ToSpherical()
        {
            var sph = new SphericalPoint();
            sph.Longitude = this.Longitude * Math.PI / 180.0;
            sph.CoLatitude = this.Latitude * Math.PI / 180.0;
            if ((sph.CoLatitude < (Math.PI / 2.0)) && (sph.CoLatitude > (-Math.PI / 2.0)))
                sph.CoLatitude = Math.Atan(C * Math.Sin(sph.CoLatitude) / Math.Cos(sph.CoLatitude));
            sph.CoLatitude = (Math.PI / 2.0) - sph.CoLatitude;
            return sph;
        }

        /// <summary>
        /// Gets the great circle distance, initial course and final course from the current location to
        /// a location specified by the `To` point.
        /// </summary>
        /// <param name="to">The destination geographic point.</param>
        /// <returns>A <see cref="GeographicResult"/> with distance, initial course and final course.</returns>
        public GeographicResult DistanceTo(GeographicPoint to)
        {
            var gcResult = this.ToSpherical().SphericalCourseDistance(to.ToSpherical());
            return ConvertToDegreeAndNM(gcResult);
        }

        /// <summary>
        /// Converts the spherical result to degrees and nautical miles.
        /// </summary>
        /// <param name="dist">The distance in radians.</param>
        /// <param name="iCourse">The initial course in radians.</param>
        /// <param name="rCourse">The final course in radians.</param>
        /// <returns>A <see cref="GeographicResult"/> with distance, initial course and final course in degrees and nautical miles.</returns>
        private GeographicResult ConvertToDegreeAndNM(double dist, double iCourse, double rCourse)
        {
            if (Math.Abs(rCourse - Math.PI) < 1.0e-10)
            {
                rCourse = 0;
            }
            else
            {
                if (rCourse > Math.PI)
                {
                    rCourse -= Math.PI;
                }
                else
                {
                    rCourse += Math.PI;
                }
            }

            return new GeographicResult()
            {
                Distance = dist * 180.0 / Math.PI * 60.0,
                InitialCourse = (iCourse + rCourse) * 0.5 * 180.0 / Math.PI,
                FinalCourse = rCourse * 180.0 / Math.PI
            };
        }

        /// <summary>
        /// Converts the spherical result to degrees and nautical miles.
        /// </summary>
        /// <param name="gcResult">The spherical result.</param>
        /// <returns>A <see cref="GeographicResult"/> with distance, initial course and final course in degrees and nautical miles.</returns>
        private GeographicResult ConvertToDegreeAndNM(SphericalResult gcResult)
        {
            if (Math.Abs(gcResult.RgcCourse - Math.PI) < 1.0e-10)
            {
                gcResult.RgcCourse = 0;
            }
            else
            {
                if (gcResult.RgcCourse > Math.PI)
                {
                    gcResult.RgcCourse -= Math.PI;
                }
                else
                {
                    gcResult.RgcCourse += Math.PI;
                }
            }

            return new GeographicResult()
            {
                Distance = gcResult.Distance * 180.0 / Math.PI * 60.0,
                InitialCourse = (gcResult.IgcCourse + gcResult.RgcCourse) * 0.5 * 180.0 / Math.PI,
                FinalCourse = gcResult.RgcCourse * 180.0 / Math.PI
            };
        }

        /// <summary>
        /// Gets a geographic point that is `distance` nautical miles away on a specified `heading`
        /// from the current point.
        /// </summary>
        /// <param name="course">Magnetic course.</param>
        /// <param name="distance">Nautical Mile distance.</param>
        /// <returns>Returns a geographic point located distance NM on course heading.</returns>
        public GeographicPoint GetDestinationPoint(double course, double distance)
        {
            course = course * Math.PI / 180.0;
            distance = distance * Math.PI / 180.0 / 60.0;
            var dest = this.ToSpherical().SphericalDeadReckoning(course, distance);
            return dest.ToGeographic();
        }

        /// <summary>
        /// Gets the time zone (IANA) in which this point lies.
        /// </summary>
        /// <returns>IANA Time zone Id.</returns>
        public string GetTimezone()
        {
            return TimeZoneLookup.GetTimeZone(this.Latitude, this.Longitude).Result;
        }

        /// <summary>
        /// Converts the geographic point to a NetTopologySuite Point.
        /// </summary>
        /// <returns>A <see cref="Point"/> representing the geographic point.</returns>
        public Point ToGeometryPoint()
        {
            var gf = NtsGeometryServices.Instance.CreateGeometryFactory(4326);
            return gf.CreatePoint(new Coordinate(this.Longitude, this.Latitude));
        }
    }
}