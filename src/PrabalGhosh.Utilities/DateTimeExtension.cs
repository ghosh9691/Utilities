using System;
using System.Globalization;

namespace PrabalGhosh.Utilities
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

        public static long ToUnixEpoch(this DateTime value)
        {
            var result = value - DateTime.UnixEpoch;
            return (long)result.TotalMilliseconds;
        }

        public static DateTime GetMonthStart(this DateTime value, string month, string year, bool isUtc = true)
        {
            if ((month.Length != 3) && (year.Length != 4))
            {
                return DateTime.MinValue;
            }

            try
            {
                var m = DateTime.ParseExact(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(month),
                    "MMM", CultureInfo.InvariantCulture).Month;
                var y = Convert.ToInt32(year);
                return new DateTime(y, m, 1, 0, 0, 0, isUtc ? DateTimeKind.Utc : DateTimeKind.Local);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e.Message);
                return DateTime.MinValue;
            }
        }
        public static DateTime GetMonthEnd(this DateTime value, string month, string year, bool isUtc = true)
        {
            if ((month.Length != 3) && (year.Length != 4))
            {
                return DateTime.MinValue;
            }

            try
            {
                var m = DateTime.ParseExact(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(month),
                    "MMM", CultureInfo.InvariantCulture).Month;
                var y = Convert.ToInt32(year);
                var lastDay = DateTime.DaysInMonth(y, m);
                return new DateTime(y, m, lastDay, 0, 0, 0, isUtc ? DateTimeKind.Utc : DateTimeKind.Local);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e.Message);
                return DateTime.MinValue;
            }
        }
    }
}