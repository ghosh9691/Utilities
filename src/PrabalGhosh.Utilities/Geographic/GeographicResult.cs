namespace PrabalGhosh.Utilities.Geographic
{
    public class GeographicResult
    {
        // Distance in meters
        public double Distance { get; set; }

        public double FinalCourse { get; set; }
        public double InitialCourse { get; set; }

    }

    public static class GeographicResultExtensions
    {
        private const double ConvertToKilometers = 0.001;
        private const double ConvertToNauticalMiles = 0.000539957;
        private const double ConvertToStatuteMiles = 0.000621371;
        
        
        public static double ToKilometers(this GeographicResult value)
        {
            return value.Distance * ConvertToKilometers;
        }

        public static double ToNauticalMiles(this GeographicResult value)
        {
            return value.Distance * ConvertToNauticalMiles;
        }

        public static double ToStatuteMiles(this GeographicResult value)
        {
            return value.Distance * ConvertToStatuteMiles;
        }
    }
}