using System;
using System.Runtime.InteropServices;

namespace PrabalGhosh.Utilities.Geographic
{
    /// <summary>
    /// A SphericalPoint is a representation of a point on the earth's surface
    /// expressed in terms of spherical trigonometry.
    /// </summary>
    public class SphericalPoint
    {
        // World Geodetic System (WGS) - 84
        // A = 6378137.0 m => Semi-major axis of Earth
        // B = 6356752.314245 m => Semi-minor axis of Earth
        // Inverse Flattening => (1/f) = 298.257223563
        // C = B/A
        private const double B = 1.003364089859676; //inverse complement of ellipticity - WGS84
        public double CoLatitude { get; set; }
        public double Longitude { get; set; }

        public GeographicPoint ToGeographic()
        {
            var geo = new GeographicPoint();
            geo.Latitude = Math.PI / 2.0 - this.CoLatitude;
            if ((geo.Latitude < Math.PI / 2.0) && (geo.Latitude > (-Math.PI / 2.0)))
            {
                geo.Latitude = Math.Atan(B * Math.Sin(geo.Latitude) / Math.Cos(geo.Latitude));
            }

            geo.Latitude = geo.Latitude * 180.0 / Math.PI;
            geo.Longitude = this.Longitude * 180.0 / Math.PI;
            return geo;
        }

        public SphericalResult SphericalCourseDistance(SphericalPoint to)
        {
            var gcDistance = 0.0;
            var igcCourse = 0.0;
            var rgcCourse = 0.0;
            var centeredAtEquator = false;

            //handle equal points...
            if (this.CoLatitude.Equals(to.CoLatitude) && this.Longitude.Equals(to.Longitude))
            {
                return new SphericalResult(gcDistance, igcCourse, rgcCourse);
            }

            //Handle crossing 180° meridian
            var dLon = to.Longitude - this.Longitude;
            if (dLon < -Math.PI)
                dLon += (Math.PI * 2.0);
            if (dLon > Math.PI)
                dLon -= (Math.PI * 2.0);
            if (Math.Abs(dLon) < 1.0e-9)
                dLon = 0.0;
            var halfAA = dLon * 0.5;
            //if either colat is at either pole
            if (halfAA == 0
                || (this.CoLatitude <= 0.0000001)
                || (to.CoLatitude <= 0.0000001)
                || (this.CoLatitude > (Math.PI - 0.0000001))
                || (to.CoLatitude > (Math.PI - 0.0000001)))
            {
                //handle spherical polar cases
                gcDistance = to.CoLatitude - this.CoLatitude;
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
                var halfBpc = (to.CoLatitude + this.CoLatitude) * 0.5;
                var cosHalfBpc = Math.Cos(halfBpc);
                //if segment centered at equator
                if (Math.Abs(cosHalfBpc) < 1.0e-7)
                {
                    centeredAtEquator = true;
                    if (Math.Abs(to.CoLatitude - this.CoLatitude) < 1.0e-5)
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

                        return new SphericalResult(gcDistance, igcCourse, rgcCourse);
                    }

                    halfAA *= 0.5;
                    to.CoLatitude = Math.PI * 0.5;
                    halfBpc = (to.CoLatitude + this.CoLatitude) * 0.5;
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
                var halfBmc = (to.CoLatitude - this.CoLatitude) * 0.5;
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

            return new SphericalResult(gcDistance, igcCourse, rgcCourse);
        }

        public SphericalPoint SphericalDeadReckoning(double course, double distance)
        {
            var sTo = new SphericalPoint();
            var sFrom = this;
            //if initial course is due north or south
            if ((course < 0.000017) || 
                ((2.0 * Math.PI - course) < 0.000017) ||
                (Math.Abs(course - Math.PI) < 0.000017))
            {
                //handle due north/south cases
                sTo.Longitude = sFrom.Longitude;
                if ((course < 0.000017) || ((2.0 * Math.PI - course) < 0.000017))
                {
                    sTo.CoLatitude = sFrom.CoLatitude - distance;
                }
                else
                {
                    sTo.CoLatitude = sFrom.CoLatitude + distance;
                }
            }
            else
            {
                //proceed with 3rd & 4th Napier analogies
                if (course > Math.PI)
                    course -= (2.0 * Math.PI);
                var halfAA = course * 0.5;
                var sinHalfAA = Math.Sin(halfAA);
                var cosHalfAA = Math.Cos(halfAA);
                var halfBpc = (sFrom.CoLatitude + distance) * 0.5;
                var halfBmc = (sFrom.CoLatitude - distance) * 0.5;
                var sinHalfBpc = Math.Sin(halfBpc);
                var cosHalfBpc = Math.Cos(halfBpc);
                var halfBBpCC = Math.Atan(cosHalfAA / sinHalfAA * Math.Cos(halfBmc) / cosHalfBpc);
                if (cosHalfBpc < 0)
                    halfBBpCC += Math.PI;
                var halfBBmCC = Math.Atan(cosHalfAA / sinHalfAA * Math.Sin(halfBmc) / sinHalfBpc);
                if (sinHalfBpc < 0)
                    halfBBmCC += Math.PI;
                var cosHalfBBpCC = Math.Cos(halfBBpCC);
                //if sum of difference of longitude and reverse gc course is a half circle,
                //handle spherical dead reckoning cases
                if (Math.Abs(cosHalfBBpCC) < 0.000001)
                {
                    //should not happen
                    sTo.CoLatitude = sFrom.CoLatitude - Math.Cos(course) * Math.PI * 0.5;
                    if (course > 0)
                    {
                        sTo.Longitude = sFrom.Longitude + distance;
                    }
                    else
                    {
                        sTo.Longitude = sFrom.Longitude - distance;
                    }
                }
                else
                {
                    //use 1st and 2nd Gaussian analogies to get CoLat
                    var sinHalfA = sinHalfAA * sinHalfBpc / Math.Cos(halfBBmCC);
                    var cosHalfA = sinHalfAA * cosHalfBpc / cosHalfBBpCC;
                    sTo.CoLatitude = 2.0 * Math.Atan(sinHalfA / cosHalfA);
                    sTo.Longitude = sFrom.Longitude + (halfBBpCC - halfBBmCC);
                }
            }
            //ensure colat is between 0 and PI
            if (sTo.CoLatitude > Math.PI)
            {
                sTo.CoLatitude = 2.0 * Math.PI - sTo.CoLatitude;
                if (sTo.Longitude < 0)
                {
                    sTo.Longitude += Math.PI;
                }
                else
                {
                    sTo.Longitude -= Math.PI;
                }
            }

            if (sTo.CoLatitude < 0)
            {
                sTo.CoLatitude = -sTo.CoLatitude;
                if (sTo.Longitude < 0)
                {
                    sTo.Longitude += Math.PI;
                }
                else
                {
                    sTo.Longitude -= Math.PI;
                }
            }

            //ensure longitude is between -PI and PI
            if (sTo.Longitude > Math.PI)
                sTo.Longitude -= (2.0 * Math.PI);
            if (sTo.Longitude < -Math.PI)
                sTo.Longitude += (2.0 * Math.PI);
            return sTo;
        }

        public static SphericalIntersection SphericalIntersection(
                SphericalPoint l1p1, SphericalPoint l1p2,
                SphericalPoint l2p1, SphericalPoint l2p2)
        {
            var intDist = -1.0;
            var forwardCrossing = false;
            var intersectionPossible = false;
            if (l1p1.CoLatitude <= l1p2.CoLatitude)
            {
                if (l2p1.CoLatitude <= l2p2.CoLatitude)
                {
                    if ((l1p2.CoLatitude >= l2p1.CoLatitude) && (l1p1.CoLatitude <= l2p2.CoLatitude))
                        intersectionPossible = true;
                }
                else
                {
                    if ((l1p2.CoLatitude >= l2p2.CoLatitude) && (l1p1.CoLatitude <= l2p1.CoLatitude))
                        intersectionPossible = true;
                }
            }
            else
            {
                if (l2p1.CoLatitude <= l2p2.CoLatitude)
                {
                    if ((l1p1.CoLatitude >= l2p1.CoLatitude) && (l1p2.CoLatitude <= l2p2.CoLatitude))
                        intersectionPossible = true;
                }
                else
                {
                    if ((l1p1.CoLatitude >= l2p2.CoLatitude) && (l1p2.CoLatitude <= l2p1.CoLatitude))
                        intersectionPossible = true;
                }
            }
            if (!intersectionPossible)
                return new SphericalIntersection
                {
                    Distance = -1, 
                    SphericalLocation = null
                };
            var gcr1 = l1p1.SphericalCourseDistance(l1p2);
            var gcr2 = l1p1.SphericalCourseDistance(l2p1);
            var gcr3 = l1p1.SphericalCourseDistance(l2p2);
            var gcr4 = l2p1.SphericalCourseDistance(l2p2);
            CheckForIntersection(
                gcr1.IgcCourse,
                gcr1.Distance,
                gcr2.Distance,
                gcr2.IgcCourse,
                gcr3.IgcCourse,
                gcr2.RgcCourse,
                gcr4.IgcCourse,
                out intDist,
                out forwardCrossing
                );
            if (intDist > 0)
            {
                var intPoint = l1p1.SphericalDeadReckoning(gcr1.IgcCourse, intDist);
                return new SphericalIntersection()
                {
                    Distance = intDist,
                    SphericalLocation = intPoint
                };
            }

            return new SphericalIntersection();
        }

        private static void CheckForIntersection(double igc0, double dist0, double dist1,
            double igc1, double igc2, double rgc1, double igcLine2,
            out double intersection, out bool forwardCrossing)
        {
            intersection = -1.0;
            forwardCrossing = true;
            double d1 = 0.0; 
            double B = 0.0; 
            var r270 = Math.PI * 1.5;
            var r90 = Math.PI * 0.5;
            var slack = 0.001;      //a little more than 3 miles
            if (Math.Abs(igc1 - igc0) < 0.00001)
                igc0 = igc1;
            if (Math.Abs(igc2 - igc0) < 0.00001)
                igc0 = igc2;
            var intersectionPossible = false;
            if (igc1 > Math.PI)
            {
                if ((igc2 > igc1) || (igc2 < (igc1 - Math.PI)))
                {
                    if (igc2 > igc1)
                    {
                        if ((igc0 >= igc1) && (igc0 <= igc2))
                        {
                            d1 = igc0 - igc1;
                            B = rgc1 - igcLine2;
                            if (B < 0)
                                B = (2.0 * Math.PI) + B;
                            intersectionPossible = true;
                            forwardCrossing = true;
                        }
                    }
                    else
                    {
                        if ((igc0 >= 0) && (igc0 <= igc2))
                        {
                            d1 = (Math.PI * 2.0 - igc1) + igc0;
                            B = rgc1 - igcLine2;
                            if (B < 0)
                                B = 2.0 * Math.PI + B;
                            intersectionPossible = true;
                            forwardCrossing = true;
                        }

                        if ((igc0 >= igc1) && (igc0 <= (2.0 * Math.PI)))
                        {
                            d1 = igc0 - igc1;
                            B = rgc1 - igcLine2;
                            if (B < 0)
                                B = 2.0 * Math.PI + B;
                            intersectionPossible = true;
                            forwardCrossing = true;
                        }
                    }
                }
                else
                {
                    if ((igc0 >= igc2) && (igc0 <= igc1))
                    {
                        d1 = igc1 - igc0;
                        B = igcLine2 - rgc1;
                        if (B < 0)
                            B = 2.0 * Math.PI + B;
                        intersectionPossible = true;
                        forwardCrossing = false;
                    }
                }
            }
            else
            {
                if ((igc2 < igc1) || (igc2 > (igc1 + Math.PI)))
                {
                    if (igc2 < igc1)
                    {
                        if ((igc0 <= igc1) && (igc0 >= igc2))
                        {
                            d1 = igc1 - igc0;
                            B = igcLine2 - rgc1;
                            if (B < 0)
                                B = 2.0 * Math.PI + B;
                            intersectionPossible = true;
                            forwardCrossing = false;
                        }
                    }
                    else
                    {
                        if ((igc0 >= 0) && (igc0 <= igc1))
                        {
                            d1 = igc1 - igc0;
                            B = igcLine2 - rgc1;
                            if (B < 0)
                                B = 2.0 * Math.PI + B;
                            intersectionPossible = true;
                            forwardCrossing = false;
                        }

                        if ((igc0 >= igc2) && (igc0 <= (2.0 * Math.PI)))
                        {
                            d1 = (2.0 * Math.PI - igc0) + igc1;
                            B = igcLine2 - rgc1;
                            if (B < 0)
                                B = 2.0 * Math.PI + B;
                            intersectionPossible = true;
                            forwardCrossing = false;
                        }
                    }
                }
                else
                {
                    if ((igc1 <= igc0) && (igc0 <= igc2))
                    {
                        d1 = igc0 - igc1;
                        B = rgc1 - igcLine2;
                        if (B < 0)
                            B = 2.0 * Math.PI + B;
                        intersectionPossible = true;
                        forwardCrossing = true;
                    }
                }
            }

            if (intersectionPossible)
            {
                intersection = DistanceToIntersection(B, d1, dist1);
                if (intersection > (dist0 + slack))
                    intersection = -1.0;
            }
            else
            {
                intersection = -1.0;
            }
        }

        private static double DistanceToIntersection(double B, double C, double a)
        {
            var cosBpC2 = Math.Cos((B + C) * 0.5);
            if (cosBpC2 == 0)
                return -1.0;
            var sinBpC2 = Math.Sin((B + C) * 0.5);
            if (sinBpC2 == 0)
                return -1.0;
            var cosA2 = Math.Cos(a * 0.5);
            if (cosA2 == 0)
                return -1.0;
            var sinA2 = Math.Sin(a * 0.5);
            var tanBpC2 = sinA2 / cosA2 * Math.Cos((B - C) * 0.5) / cosBpC2;
            var tanBmC2 = sinA2 / cosA2 * Math.Sin((B - C) * 0.5) / sinBpC2;
            return Math.Atan(tanBpC2) + Math.Atan(tanBmC2);
        }
    }
}