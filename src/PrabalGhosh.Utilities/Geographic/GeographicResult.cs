using System;

namespace PrabalGhosh.Utilities.Geographic
{
    public class GeographicResult
    {
        // Distance in Nautical Miles
        public double Distance { get; set; }
        public double FinalCourse { get; set; }
        public double InitialCourse { get; set; }

        public GeographicPoint Location { get; set; }
    }
}