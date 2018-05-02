using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Flitesys.Utilities
{
    public static class StringExtensions
    {
        public static string GetHash(this string value)
        {
            using (var sha = SHA256.Create())
            {
                byte[] hashed = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
                return BitConverter.ToString(hashed).Replace("-", "");
            }
        }

        public static string GetHash(this string value, string salt)
        {
            using (var sha = SHA256.Create())
            {
                byte[] hashed = sha.ComputeHash(Encoding.UTF8.GetBytes(value + salt));
                return BitConverter.ToString(hashed).Replace("-", "");
            }
        }

        public static string ToBase64(this string value)
        {
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytesToEncode);
        }

        public static string FromBase64(this string value)
        {
            while ((value.Length % 4) != 0)
            {
                value += "=";
            }
            byte[] decoded = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(decoded);
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static double FromAiracLatitude(this string value)
        {
            if (value.Length != 9)
                throw new ArgumentException($"{value} is not an AIRAC Latitude!");
            if ((value[0] != 'N') && (value[0] != 'S'))
                throw new ArgumentException($"{value} does not have hemisphere information!");
            try
            {
                int deg = Convert.ToInt32(value.Substring(1, 2));
                int min = Convert.ToInt32(value.Substring(3, 2));
                int sec = Convert.ToInt32(value.Substring(5, 2));
                int hSec = Convert.ToInt32(value.Substring(7, 2));
                double target = deg + min / 60.0 + sec / 3600.0 + hSec / 360000.0;
                if (value[0] == 'S')
                    target = -target;
                return target;
            }
            catch
            {
                return -91.0;
            }
        }

        public static double FromAiracLongitude(this string value)
        {
            if (value.Length != 10)
                throw new ArgumentException($"{value} is not an AIRAC Longitude!");
            if ((value[0] != 'E') && (value[0] != 'W'))
                throw new ArgumentException($"{value} does not have hemisphere information!");
            try
            {
                int deg = Convert.ToInt32(value.Substring(1, 3));
                int min = Convert.ToInt32(value.Substring(4, 2));
                int sec = Convert.ToInt32(value.Substring(6, 2));
                int hSec = Convert.ToInt32(value.Substring(8, 2));
                double target = deg + min / 60.0 + sec / 3600.0 + hSec / 360000.0;
                if (value[0] == 'W')
                    target = -target;
                return target;
            }
            catch
            {
                return -181.0;
            }
        }

        public static DateTime AiracEffectiveDate(this string value)
        {
            var airac = new Airac(value);
            return airac.GetEffectiveDate();
        }

        public static DateTime AiracDiscontinueDate(this string value)
        {
            var airac = new Airac(value);
            return airac.GetDiscontinueDate();
        }

        public static int FromAiracElevation(this string value)
        {
            if (value.Length == 5)
            {
                try
                {
                    return Convert.ToInt32(value);
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        public static double FromMagneticVariation(this string value)
        {
            try
            {
                if (value.Length == 5)
                {
                    if (value[0] == 'T')
                        return Convert.ToDouble(value.Substring(1, 4)) / 10.0;
                    var magVar = Convert.ToDouble(value.Substring(1, 4)) / 10.0;
                    if (value[0] == 'W')
                        return -magVar;
                    return magVar;
                }
                return 0.0;
            }
            catch (Exception)
            {
                return 0.0;
            }
        }
    }
}