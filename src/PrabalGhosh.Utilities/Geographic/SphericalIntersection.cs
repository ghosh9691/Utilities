namespace PrabalGhosh.Utilities.Geographic
{
    public class SphericalIntersection
    {
        public double Distance { get; set; }
        public SphericalPoint SphericalLocation { get; set; }

        public SphericalIntersection()
        {
            Distance = -1.0;
            SphericalLocation = null;
        }
    }
}