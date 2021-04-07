using System;
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
        private const double C = 0.996647189296812; //complement of ellipticity - WGS84
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

        public GeographicResult GetGreatCircle(GeographicPoint to)
        {
            var gcDistance = 0.0;
            var igcCourse = 0.0;
            var rgcCourse = 0.0;
            var centeredAtEquator = false;
            
            var sFrom = this.ToSpherical();
            var sTo = to.ToSpherical();
            
            //handle equal points...
            if (sFrom.CoLatitude.Equals(sTo.CoLatitude) && sFrom.Longitude.Equals(sTo.Longitude))
            {
                return ConvertToDegreeAndNauticalMile(gcDistance, igcCourse, rgcCourse);
            }
            //Handle crossing 180° meridian
            var dLon = sTo.Longitude - sFrom.Longitude;
            if (dLon < -Math.PI)
                dLon += (Math.PI * 2.0);
            if (dLon > Math.PI)
                dLon -= (Math.PI * 2.0);
            if (Math.Abs(dLon) < 1.0e-9)
                dLon = 0.0;
            var halfAA = dLon * 0.5;
            //if either colat is at either pole
            if (halfAA == 0
                || (sFrom.CoLatitude <= 0.0000001)
                || (sTo.CoLatitude <= 0.0000001)
                || (sFrom.CoLatitude > (Math.PI - 0.0000001))
                || (sTo.CoLatitude > (Math.PI - 0.0000001)))
            {
                //handle spherical polar cases
                gcDistance = sTo.CoLatitude - sFrom.CoLatitude;
                if (gcDistance < 0)
                {
                    gcDistance = -gcDistance;
                    igcCourse = 0;
                    rgcCourse = Math.PI;
                }
                else
                {
                    if (gcDistance == 0)
                    {
                        igcCourse = 0;
                        rgcCourse = 0;
                    }
                    else
                    {
                        igcCourse = Math.PI;
                        rgcCourse = 0;
                    }
                }
            }
            else
            {
                //compute using 3rd and 4th Napier analogies
                var halfBpc = (sTo.CoLatitude + sFrom.CoLatitude) * 0.5;
                var cosHalfBpc = Math.Cos(halfBpc);
                //if segment centered at equator
                if (Math.Abs(cosHalfBpc) < 1.0e-7)
                {
                    centeredAtEquator = true;
                    if (Math.Abs(sTo.CoLatitude - sFrom.CoLatitude) < 1.0e-5)
                    {
                        //along the equator
                        gcDistance = dLon;
                        if (gcDistance < 0)
                        {
                            igcCourse = Math.PI * 1.5;
                            rgcCourse = Math.PI * 0.5;
                            gcDistance = -gcDistance;
                        }
                        else
                        {
                            if (gcDistance == 0)
                            {
                                igcCourse = 0.0;
                                rgcCourse = 0.0;
                            }
                            else
                            {
                                igcCourse = Math.PI * 0.5;
                                rgcCourse = Math.PI * 1.5;
                            }
                        }

                        return ConvertToDegreeAndNauticalMile(gcDistance, igcCourse, rgcCourse);
                    }

                    halfAA *= 0.5;
                    sTo.CoLatitude = Math.PI * 0.5;
                    halfBpc = (sTo.CoLatitude + sFrom.CoLatitude) * 0.5;
                    cosHalfBpc = Math.Cos(halfBpc);
                }
                else
                {
                    centeredAtEquator = false;
                }

                //use 1st and 2nd Gaussian Analogies
                var sinHalfAA = Math.Sin(halfAA);
                var cosHalfAA = Math.Cos(halfAA);
                var sinHalfBpc = Math.Sin(halfBpc);
                var halfBmc = (sTo.CoLatitude - sFrom.CoLatitude) * 0.5;
                var halfBBpCC = Math.Atan(cosHalfAA / sinHalfAA * Math.Cos(halfBmc) / cosHalfBpc);
                var halfBBmCC = Math.Atan(cosHalfAA / sinHalfAA * Math.Sin(halfBmc) / sinHalfBpc);
                var cosHalfBBpCC = Math.Cos(halfBBpCC);
                var sinHalfA = sinHalfAA * sinHalfBpc / Math.Cos(halfBBmCC);
                var cosHalfA = sinHalfAA * cosHalfBpc / cosHalfBBpCC;
                igcCourse = halfBBpCC + halfBBmCC;
                if (centeredAtEquator)
                {
                    gcDistance = 4 * Math.Atan(sinHalfA / cosHalfA);
                    if (igcCourse < Math.PI)
                        rgcCourse = igcCourse + Math.PI;
                    else
                    {
                        rgcCourse = igcCourse - Math.PI;
                    }
                }
                else
                {
                    gcDistance = 2 * Math.Atan(sinHalfA / cosHalfA);
                    rgcCourse = halfBBpCC - halfBBmCC;
                }

                if (gcDistance < 0)
                    gcDistance = -gcDistance;
                if ((igcCourse * dLon) < 0)
                {
                    if (igcCourse < 0)
                    {
                        igcCourse += Math.PI;
                    }
                    else
                    {
                        igcCourse -= Math.PI;
                    }
                }

                if ((rgcCourse * dLon) > 0)
                {
                    rgcCourse = -rgcCourse;
                }
                else
                {
                    if (rgcCourse < 0)
                    {
                        rgcCourse = -Math.PI - rgcCourse;
                    }
                    else
                    {
                        rgcCourse = Math.PI - rgcCourse;
                    }
                }
                //put courses in positive bearings...
                if (igcCourse < 0)
                    igcCourse += (Math.PI * 2.0);
                if (igcCourse > (2.0 * Math.PI - 0.000017))
                    igcCourse = 0;
                if (rgcCourse < 0)
                    rgcCourse += (Math.PI * 2.0);
                if (rgcCourse > (2.0 * Math.PI - 0.000017))
                    rgcCourse = 0;
            }

            return ConvertToDegreeAndNauticalMile(gcDistance, igcCourse, rgcCourse);
        }

        private GeographicResult ConvertToDegreeAndNauticalMile(double dist, double iCourse, double rCourse)
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
                FinalCourse = rCourse
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