using System.Linq;
using System.Security.Cryptography;

namespace Flitesys.Utilities
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

        private static byte RandomByte()
        {
            using (var randomizer = RandomNumberGenerator.Create())
            {
                var randomBytes = new byte[1];
                randomizer.GetBytes(randomBytes);
                return randomBytes.Single();
            }
        }
    }
}