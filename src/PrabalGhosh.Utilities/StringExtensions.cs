using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using PrabalGhosh.Utilities.Aviation;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Linq;
using Konscious.Security.Cryptography;

namespace PrabalGhosh.Utilities
{
    public static class StringExtensions
    {
        public static DateTime AiracDiscontinueDate(this string value)
        {
            var airac = new Airac(value);
            return airac.GetDiscontinueDate();
        }

        public static DateTime AiracEffectiveDate(this string value)
        {
            var airac = new Airac(value);
            return airac.GetEffectiveDate();
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

        public static double FromAiracLatitude(this string value)
        {
            if (value.Length != 9)
                return -91.0;
            if ((value[0] != 'N') && (value[0] != 'S'))
                return -91.0;
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
                return -181.0;
            if ((value[0] != 'E') && (value[0] != 'W'))
                return -181.0;
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

        public static string FromBase64(this string value)
        {
            while ((value.Length % 4) != 0)
            {
                value += "=";
            }

            byte[] decoded = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(decoded);
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

        [Obsolete("Use GetSecureHash() instead...")]
        public static string GetHash(this string value)
        {
            using (var sha = SHA256.Create())
            {
                byte[] hashed = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
                return BitConverter.ToString(hashed).Replace("-", "");
            }
        }

        [Obsolete("Use GetSecureHash() instead...")]
        public static string GetHash(this string value, string salt)
        {
            using (var sha = SHA256.Create())
            {
                byte[] hashed = sha.ComputeHash(Encoding.UTF8.GetBytes(value + salt));
                return BitConverter.ToString(hashed).Replace("-", "");
            }
        }
        

        public static string GetSecureHash(this string value, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(value));
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8;
            argon2.Iterations = 40;
            argon2.MemorySize = 8192;   //in KiB
            var result = argon2.GetBytes(64);   //512-bit hash
            return Convert.ToBase64String(result);
        }

        public static string GetSecureHash(this string value, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            return GetSecureHash(value, saltBytes);
        }
        

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static string ToBase64(this string value)
        {
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytesToEncode);
        }

        public static Int32 ToInt32(this string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return -1;
            }
        }

        public static Int32? ToNullableInt32(this string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return null;
            }
        }

        public static Int64 ToInt64(this string value)
        {
            try
            {
                return Convert.ToInt64(value);
            }
            catch
            {
                return -1;
            }
        }

        public static Int64? ToNullableInt64(this string value)
        {
            try
            {
                return Convert.ToInt64(value);
            }
            catch
            {
                return -1;
            }
        }

        public static double ToDouble(this string value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0.0;
            }
        }

        public static double? ToNullableDouble(this string value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return null;
            }
        }

        public static bool IsDigits(this string value)
        {
            try
            {
                int i = 0;
                return int.TryParse(value, out i);
            }
            catch
            {
                return false;
            }
        }

        public static string GetId(this string value)
        {
            return DateTime.UtcNow.Ticks.ToString("X");
        }

        public static string RemoveUnwantedCharacters(this string value, string allowedCharactersRegEx)
        {
            return Regex.Replace(value, allowedCharactersRegEx, string.Empty);
        }

        public static string RemoveSpecialCharacters(this string value)
        {
            return Regex.Replace(value, @"[^0-9a-zA-Z-_.]", string.Empty);
        }

        public static bool IsValidEmail(this string value)
        {
            
            if (string.IsNullOrWhiteSpace(value))
                return false;

            try
            {
                // Normalize the domain
                value = Regex.Replace(value, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(value,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);
                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;
                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}