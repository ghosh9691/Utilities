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
    }
}