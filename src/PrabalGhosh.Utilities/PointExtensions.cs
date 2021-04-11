using GeoTimeZone;
using NetTopologySuite.Geometries;
using PrabalGhosh.Utilities.Geographic;

namespace PrabalGhosh.Utilities
{
    public static class PointExtensions
    {
        public static string GetIanaTimeZone(this Point value)
        {
            return TimeZoneLookup.GetTimeZone(value.X, value.Y).Result;
        }

        public static GeographicResult DistanceTo(this Point src, Point dest)
        {
            var origin = new GeographicPoint(src);
            var destination = new GeographicPoint(dest);
            return origin.DistanceTo(destination);
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