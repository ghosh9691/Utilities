namespace PrabalGhosh.Utilities.Geographic
{
    public class SphericalResult
    {
        public double Distance { get; set; }
        public double IgcCourse { get; set; }
        public double RgcCourse { get; set; }
        
        public SphericalResult()
        {}

        public SphericalResult(double dist, double iCourse, double rCourse)
        {
            Distance = dist;
            IgcCourse = iCourse;
            RgcCourse = rCourse;
        }
    }
}