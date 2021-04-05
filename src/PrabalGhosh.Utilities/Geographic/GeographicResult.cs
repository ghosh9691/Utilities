using System;

namespace PrabalGhosh.Utilities.Geographic
{
    public class GeographicResult
    {
        private double _finalCourse;

        private double _initialCourse;

        private double _distance;
        
        // Distance in meters
        public double Distance
        {
            get
            {
                return Math.Round(_distance, 2);
            }
            set => _distance = value;
        }

        public double FinalCourse {
            get
            {
                return Math.Round(_finalCourse);
            }
            set => _finalCourse = value;
        }
        public double InitialCourse {
            get
            {
                return Math.Round(_initialCourse);
            }
            set => _initialCourse = value;
        }
        
        public GeographicPoint Coords { get; set; }

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