using NetTopologySuite.Geometries;

namespace PrabalGhosh.Utilities.Geographic
{
    public static class PointExtensions
    {
        public static GeographicResult DistanceTo(this Point value, Point to)
        {
            return (new GeographicPoint(value).DistanceTo(new GeographicPoint(to)));
        }

        public static GeographicLine GetLine(this Point value, Point to)
        {
            return new GeographicLine(value, to);
        }

        public static GeographicPoint GetPoint(this Point value)
        {
            return new GeographicPoint(value);
        }
    }
}