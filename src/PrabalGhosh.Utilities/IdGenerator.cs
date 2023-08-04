using System;
using System.Linq;
using System.Security.Cryptography;

namespace PrabalGhosh.Utilities
{
    public class IdGenerator
    {
        public static string GetId(int length = 16)
        {
            string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return RandomString(length, allowedCharacters);
        }

        public static string GetPNR(int length = 6)
        {
            string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return RandomString(length, allowedCharacters);
        }

        private static byte RandomByte()
        {
            using (var randomizer = RandomNumberGenerator.Create())
            {
                var randomBytes = new byte[1];
                randomizer.GetBytes(randomBytes);
                return randomBytes.Single();
            }
        }

        private static string RandomString(int length, string alphabet)
        {
            var outOfRange = byte.MaxValue + 1 - (byte.MaxValue + 1) % alphabet.Length;

            return string.Concat(
                Enumerable
                    .Repeat(0, int.MaxValue)
                    .Select(e => RandomByte())
                    .Where(randomByte => randomByte < outOfRange)
                    .Take(length)
                    .Select(randomByte => alphabet[randomByte % alphabet.Length])
            );
        }

        public static string GenerateSalt(int size = 16)
        {
            byte[] saltBytes = new byte[size];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }
    }
}