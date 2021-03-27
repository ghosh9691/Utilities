using GeoTimeZone;
using NetTopologySuite.Geometries;

namespace PrabalGhosh.Utilities
{
    public static class PointExtensions
    {
        public static string GetIanaTimeZone(this Point value)
        {
            return TimeZoneLookup.GetTimeZone(value.X, value.Y).Result;
        }
    }
}