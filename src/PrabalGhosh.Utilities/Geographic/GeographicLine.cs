using System;
using NetTopologySuite.Geometries;

namespace PrabalGhosh.Utilities.Geographic
{
    /// <summary>
    /// A GeographicLine represents a geodesic on the earth's surface as specified
    /// by 2 geographic points - a 'Start' or an 'End'.
    /// </summary>
    public class GeographicLine
    {
        private GeographicPoint Start { get; set; }
        private GeographicPoint End { get; set; }

        public GeographicLine()
        {
        }

        public GeographicLine(GeographicPoint start, GeographicPoint end)
        {
            this.Start = start;
            this.End = end;
        }

        public GeographicLine(Point start, Point end)
        {
            this.Start = new GeographicPoint(start);
            this.End = new GeographicPoint(end);
        }

        public GeographicResult GreatCircle()
        {
            return Start.DistanceTo(End);
        }

        public Intersection GetIntersection(GeographicLine line)
        {
            var l1p1 = this.Start.ToSpherical();
            var l1p2 = this.End.ToSpherical();
            var l2p1 = line.Start.ToSpherical();
            var l2p2 = line.End.ToSpherical();

            var sphInt = SphericalPoint.SphericalIntersection(l1p1, l1p2, l2p1, l2p2);
            if (sphInt.IsNotNull())
            {
                if (sphInt.Distance > 0)
                {
                    //valid intersection was found!
                    return new Intersection()
                    {
                        Distance = sphInt.Distance * 180.0/Math.PI * 60.0,
                        Location = sphInt.SphericalLocation.ToGeographic()
                    };
                }
            }

            return null;
        }
    }
}