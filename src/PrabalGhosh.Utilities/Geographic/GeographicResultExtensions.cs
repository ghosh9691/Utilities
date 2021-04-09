using System;

namespace PrabalGhosh.Utilities.Geographic
{
    public static class GeographicResultExtensions
    {
        private const double ConvertToKilometers = 1.852;
        private const double ConvertToStatuteMiles = 1.15078;


        public static double ToKilometers(this GeographicResult value, int precision = 2)
        {
            return Math.Round(value.Distance * ConvertToKilometers, precision);
        }

        public static double ToNauticalMiles(this GeographicResult value, int precision = 2)
        {
            return Math.Round(value.Distance, precision);
        }

        public static double ToStatuteMiles(this GeographicResult value, int precision = 2)
        {
            return Math.Round(value.Distance * ConvertToStatuteMiles, precision);
        }
    }
}