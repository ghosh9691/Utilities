using System;

namespace Flitesys.Utilities
{
    public static class DateTimeExtension
    {
        public static DateTime GetSafeMinimumDateLocal(this DateTime value)
        {
            return new DateTime(1753, 1, 1, 0, 0, 0, DateTimeKind.Local);
        }

        public static DateTime GetSafeMinimumDateZulu(this DateTime value)
        {
            return new DateTime(1753, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        public static DateTime SafeParse(this DateTime value, string formatString)
        {
            DateTime.TryParse(formatString, out DateTime parsedValue);
            if (parsedValue.IsNull())
                return GetSafeMinimumDateZulu(value);
            return parsedValue;
        }
    }
}