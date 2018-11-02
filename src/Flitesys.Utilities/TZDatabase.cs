using NodaTime.TimeZones;
using System.Collections.Generic;
using System.Linq;

namespace Flitesys.Utilities
{
    public static class TZDatabase
    {
        public static List<string> GetTimeZones()
        {
            var result = new List<string>();
            TzdbDateTimeZoneSource.Default.ZoneLocations.ToList().ForEach(tz => { result.Add(tz.ZoneId); });
            return result;
        }
    }
}