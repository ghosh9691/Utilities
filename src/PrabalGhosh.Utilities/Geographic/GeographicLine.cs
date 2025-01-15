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
        /// <summary>
        /// Gets or sets the start point of the geographic line.
        /// </summary>
        private GeographicPoint Start { get; set; }

        /// <summary>
        /// Gets or sets the end point of the geographic line.
        /// </summary>
        private GeographicPoint End { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicLine"/> class.
        /// </summary>
        public GeographicLine()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicLine"/> class with the specified start and end points.
        /// </summary>
        /// <param name="start">The start point of the geographic line.</param>
        /// <param name="end">The end point of the geographic line.</param>
        public GeographicLine(GeographicPoint start, GeographicPoint end)
        {
            this.Start = start;
            this.End = end;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicLine"/> class with the specified start and end points.
        /// </summary>
        /// <param name="start">The start point of the geographic line.</param>
        /// <param name="end">The end point of the geographic line.</param>
        public GeographicLine(Point start, Point end)
        {
            this.Start = new GeographicPoint(start);
            this.End = new GeographicPoint(end);
        }

        /// <summary>
        /// Calculates the great circle distance between the start and end points.
        /// </summary>
        /// <returns>A <see cref="GeographicResult"/> representing the great circle distance.</returns>
        public GeographicResult GreatCircle()
        {
            return Start.DistanceTo(End);
        }

        /// <summary>
        /// Gets the intersection point of this geographic line with another geographic line.
        /// </summary>
        /// <param name="line">The other geographic line.</param>
        /// <returns>An <see cref="Intersection"/> object representing the intersection point, or <c>null</c> if no valid intersection is found.</returns>
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
                    // valid intersection was found!
                    return new Intersection()
                    {
                        Distance = sphInt.Distance * 180.0 / Math.PI * 60.0,
                        Location = sphInt.SphericalLocation.ToGeographic()
                    };
                }
            }

            return null;
        }
    }
}